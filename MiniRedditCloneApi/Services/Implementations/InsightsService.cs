using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedditCloneApi.Data;
using MiniRedditCloneApi.DTOs.Insight;
using MiniRedditCloneApi.DTOs.Response;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;
using MiniRedditCloneApi.Utils;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class InsightsService(MiniRedditCloneApiDbContext dbContext, UserManager<Nerd> userManager, INsfwDetectionService nsfwDetectionService) : IInsightsService
    {
        private readonly MiniRedditCloneApiDbContext _dbContext = dbContext;
        private readonly UserManager<Nerd> _userManager = userManager;
        private readonly INsfwDetectionService _nsfwDetectionService = nsfwDetectionService;

        public async Task<Result<List<Insight>>> GetPopularInsightsAsync(ClaimsPrincipal user, int page, int pageSize)
        {
            var nerd = await _userManager.GetUserAsync(user);
            var query = _dbContext.Insights.AsQueryable().Where(insight => insight.Status == InsightStatus.Active);
            
            if (nerd is null || nerd.NsfwOption == NsfwOption.Hide)
            {
                query = query
                    .Where(insight => !insight.IsNSFW && !insight.IsAutoFlaggedNsfw);
            }

            var cutoff = DateTime.UtcNow.AddDays(-2);
            var candidates = await query
                .Include(insight => insight.InsightReactions)
                .Include(insight => insight.Notes)
                .Where(insight => insight.CreatedAt >= cutoff)
                .ToListAsync();

            var popularInsights = candidates
                .GroupBy(insight => insight.HerdId)
                .SelectMany(g => g
                    .Select(insight => new { Insight = insight, Score = InsightRank.GetInsightHotScore(insight) })
                    .OrderByDescending(x => x.Score)
                    .Take(2)
                )
                .OrderByDescending(x => x.Score)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Insight)
                .ToList();
            
            return Result<List<Insight>>.Ok(popularInsights);
        }

        public async Task<Result<Insight>> CreateInsightAsync(ClaimsPrincipal user, int herdId, CreateInsightDTO dto)
        {
            System.Console.WriteLine(herdId);
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<Insight>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds
                .Include(herd => herd.HerdDomains)
                .ThenInclude(herdDomain => herdDomain.Domain)
.ThenInclude(domain => domain.Topics)
                .FirstOrDefaultAsync(herd => herd.Id == herdId);
            if (herd is null) return Result<Insight>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);

            if (herdNerd is null) return Result<Insight>.Fail(ErrorType.Forbidden, "You must be a member of the herd to post insights.");
            if (dto.IsNSFW && !herd.AllowNSFW)
            {
                return Result<Insight>.Fail(ErrorType.BadRequest, "This herd does not allow NSFW content. Please choose a different herd.");
            }
            List<int> herdTopicIds = [];
            herd.HerdDomains.ToList().ForEach(herdDomain =>
            {
                herdDomain.Domain.Topics.ToList().ForEach(topic =>
                {
                    herdTopicIds.Add(topic.Id);
                });
            });

            var insightTopics = await _dbContext.Topics.Where(topic => dto.TopicIds.Contains(topic.Id) && herdTopicIds.Contains(topic.Id)).ToListAsync();

            if (dto.TopicIds.Count != insightTopics.Count)
            {
                return Result<Insight>.Fail(ErrorType.BadRequest, "Invalid Topic Provided");
            }

            var insight = new Insight()
            {
                Title = dto.Title,
                Text = dto.Text,
                NerdId = nerd.Id,
                HerdId = herd.Id,
                IsNSFW = dto.IsNSFW
            };

            insightTopics.ForEach(insight.Topics.Add);
            NsfwDetectionResponse? imageModeration = null;

            if (dto.Media is not null && dto.Media.Length > 0)
            {
                using var stream = dto.Media.OpenReadStream();
                var filename = Path.GetFileName(dto.Media.FileName);
                var imageModerationResult = await _nsfwDetectionService.CheckImageAsync(stream, filename, herd.AllowNSFW);

                if (!imageModerationResult.Success)
                {
                    return Result<Insight>.Fail(ErrorType.ServerError, imageModerationResult.Error!);
                }

                imageModeration = imageModerationResult.Data;
                if (imageModeration.Decision == ModerationDecision.REJECT)
                {
                    return Result<Insight>.Fail(ErrorType.BadRequest, $"{imageModeration.Signal.Category}: {imageModeration.Reason}");
                }
                using var ms = new MemoryStream();
                await dto.Media.CopyToAsync(ms);
                insight.Media = ms.ToArray();
            }

            var textModerationResult = await _nsfwDetectionService.CheckTextAsync($"{dto.Title} {dto.Text}", herd.AllowNSFW);
            if (!textModerationResult.Success)
            {
                return Result<Insight>.Fail(ErrorType.BadRequest, textModerationResult.Error!);
            }
            var textModeration = textModerationResult.Data;
            if (textModeration.Decision == ModerationDecision.REJECT)
            {
                return Result<Insight>.Fail(ErrorType.BadRequest, $"{textModeration.Signal.Category}: {textModeration.Reason}");           
            }

            if (imageModeration?.Decision == ModerationDecision.PENDING_MODERATION || textModeration.Decision == ModerationDecision.PENDING_MODERATION)
            {
                if (!herd.AllowNSFW) insight.Status = InsightStatus.PendingModeration;
                else insight.Status = InsightStatus.Active;

                insight.IsAutoFlaggedNsfw = true;
                insight.NsfwConfidence = imageModeration?.Decision == ModerationDecision.PENDING_MODERATION ? imageModeration.Signal.Confidence : textModeration.Signal.Confidence; 
            }
            await _dbContext.Insights.AddAsync(insight);
            await _dbContext.SaveChangesAsync();
            insight = await _dbContext.Insights
                .Include(i => i.InsightReactions)
                .Include(i => i.Notes)
                .FirstOrDefaultAsync(i => i.Id == insight.Id);
            return Result<Insight>.Ok(insight!);
        }

        public async Task<Result<byte[]>> GetInsightMediaAsync(int insightId)
        {
            var insight = await _dbContext.Insights.FindAsync(insightId);
            if (insight is null) return Result<byte[]>.Fail(ErrorType.NotFound, "No insight found with the given identifier.");

            if (insight.Media is null || insight.Media.Length <= 0)
                return Result<byte[]>.Fail(ErrorType.BadRequest, "This insight does not have a media.");
            
            return Result<byte[]>.Ok(insight.Media);
        }

        public async Task<Result<List<Insight>>> GetHerdInsightsAsync(ClaimsPrincipal user, int herdId, InsightSort sort, TopTimeRange? range, int page = 1, int pageSize = 50)
        {
            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<List<Insight>>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var nerd = await _userManager.GetUserAsync(user);
            var query = _dbContext.Insights.AsQueryable()
                .Include(insight => insight.InsightReactions)
                .Include(insight => insight.Notes)
                .Where(insight => insight.HerdId == herd.Id && insight.Status == InsightStatus.Active);
            
            if (nerd is null || nerd.NsfwOption == NsfwOption.Hide)
            {
                query = query
                    .Where(insight => !insight.IsNSFW && !insight.IsAutoFlaggedNsfw);
            }

            var herdInsights = sort switch
            {
                InsightSort.Hot => await GetHotInsightsAsync(query, page, pageSize),
                InsightSort.New => await GetNewInsightsAsync(query, page, pageSize),
                InsightSort.Top => await GetTopInsightsAsync(query, page, pageSize, range),
                InsightSort.Rising => await GetRisingInsightsAsync(query, page, pageSize),
                InsightSort.Controversial => await GetControversialInsightsAsync(query, page, pageSize),
                _ => await GetHotInsightsAsync(query, page, pageSize),
            };

            return Result<List<Insight>>.Ok(herdInsights);
        }

        public async Task<List<Insight>> GetHotInsightsAsync(IQueryable<Insight> query, int page, int pageSize)
        {
            var candidates = await query.ToListAsync();
            var hotInsights = candidates
                .Select(insight => new { Insight = insight, Score = InsightRank.GetInsightHotScore(insight) })
                .OrderByDescending(x => x.Score)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Insight)
                .ToList();
            return hotInsights;
        }

        public async Task<List<Insight>> GetNewInsightsAsync(IQueryable<Insight> query, int page, int pageSize)
        {
            var newInsights = await query
                .OrderByDescending(insight => insight.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return newInsights;
        }

        public async Task<List<Insight>> GetRisingInsightsAsync(IQueryable<Insight> query, int page, int pageSize)
        {
            var candidates = await query.ToListAsync();
            var risingInsights = candidates
                .Select(insight => new { Insight = insight, Score = InsightRank.GetInsightRisingScore(insight) })
                .OrderByDescending(x => x.Score)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Insight)
                .ToList();
            return risingInsights;
        }

        public async Task<List<Insight>> GetControversialInsightsAsync(IQueryable<Insight> query, int page, int pageSize)
        {
            var candidates= await query.ToListAsync();
            var controversialInsights = candidates
                .Select(insight => new { Insight = insight, Score = InsightRank.GetInsightControversialScore(insight) })
                .OrderByDescending(x => x.Score)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Insight)
                .ToList();
            return controversialInsights;
        }

        public async Task<List<Insight>> GetTopInsightsAsync(IQueryable<Insight> query, int page, int pageSize, TopTimeRange? range = TopTimeRange.Day)
        {
            var now = DateTime.UtcNow;
            var start = range switch
            {
                TopTimeRange.Hour => now.AddHours(-1),
                TopTimeRange.Day => now.AddDays(-1),
                TopTimeRange.Week => now.AddDays(-7),
                TopTimeRange.Month => now.AddMonths(-1),
                TopTimeRange.Year => now.AddYears(-1),
                TopTimeRange.All => DateTime.MinValue,
                _ => now.AddDays(-1),
            };

            var candidates = await query
                .Where(insight => insight.CreatedAt >= start)
                .ToListAsync();
            var topInsights = candidates
                .Select(insight => new { Insight = insight, Score = InsightRank.GetInsightTopScore(insight) })
                .OrderByDescending(x => x.Score)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => x.Insight)
                .ToList();
            return topInsights;
        }

        public async Task<Result<string>> ToggleInsightKudosAsync(ClaimsPrincipal user, int id)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var insight = await _dbContext.Insights.FindAsync(id);
            if (insight is null) return Result<string>.Fail(ErrorType.NotFound, "No insight found with the given identifier.");

            var insightReaction = await _dbContext.InsightReactions.FindAsync(insight.Id, nerd.Id);
            if (insightReaction is null)
            {
                await _dbContext.InsightReactions.AddAsync(new(Reaction.Kudos)
                {
                    InsightId = insight.Id,
                    NerdId = nerd.Id,
                });
            }
            else if (insightReaction.Reaction == Reaction.Crit)
            {
                insightReaction.Reaction = Reaction.Kudos;
            }
            else
            {
                _dbContext.InsightReactions.Remove(insightReaction);
            }

            await _dbContext.SaveChangesAsync();
            return Result<string>.Ok("Toggled reaction successfully.");
        }

        public async Task<Result<string>> ToggleInsightCritAsync(ClaimsPrincipal user, int id)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var insight = await _dbContext.Insights.FindAsync(id);
            if (insight is null) return Result<string>.Fail(ErrorType.NotFound, "No insight found with the given identifier.");

            var insightReaction = await _dbContext.InsightReactions.FindAsync(insight.Id, nerd.Id);
            if (insightReaction is null)
            {
                await _dbContext.InsightReactions.AddAsync(new(Reaction.Crit)
                {
                    InsightId = insight.Id,
                    NerdId = nerd.Id,
                });
            }
            else if (insightReaction.Reaction == Reaction.Kudos)
            {
                insightReaction.Reaction = Reaction.Crit;
            }
            else
            {
                _dbContext.InsightReactions.Remove(insightReaction);
            }

            await _dbContext.SaveChangesAsync();
            return Result<string>.Ok("Toggled reaction successfully.");
        }

        public async Task<Result<List<Insight>>> GetPendingInsightsAsync(ClaimsPrincipal user, int herdId)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<List<Insight>>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<List<Insight>>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator)
            {
                return Result<List<Insight>>.Fail(ErrorType.Forbidden, "You must be a moderator of the herd to access pending insights.");
            }

            var pendingInsights = await _dbContext.Insights
                .Include(insight => insight.InsightReactions)
                .Include(insight => insight.Notes)
                .Where(insight => insight.HerdId == herd.Id && insight.Status == InsightStatus.PendingModeration)
                .ToListAsync();
            
            return Result<List<Insight>>.Ok(pendingInsights);
        }

        public async Task<Result<List<Insight>>> GetModeratorRemovedInsightsAsync(ClaimsPrincipal user, int herdId)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<List<Insight>>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<List<Insight>>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator)
            {
                return Result<List<Insight>>.Fail(ErrorType.Forbidden, "You must be a moderator of the herd to access pending insights.");
            }

            var removedInsights = await _dbContext.Insights
                .Include(insight => insight.InsightReactions)
                .Include(insight => insight.Notes)
                .Where(insight => insight.HerdId == herd.Id && insight.Status == InsightStatus.RemovedByModerator)
                .ToListAsync();
            
            return Result<List<Insight>>.Ok(removedInsights);
        }

        public async Task<Result<List<Insight>>> GetMyInsightsAsync(ClaimsPrincipal user)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<List<Insight>>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var insights = await _dbContext.Insights
                .Include(insight => insight.InsightReactions)
                .Include(insight => insight.Notes)
                .Where(insight => insight.NerdId == nerd.Id)
                .ToListAsync();
            return Result<List<Insight>>.Ok(insights);
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:25 PM
# Update Fri, Jan  9, 2026  9:26:08 PM
# Update Fri, Jan  9, 2026  9:35:00 PM
