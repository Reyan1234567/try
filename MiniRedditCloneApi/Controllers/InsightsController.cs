using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniRedditCloneApi.DTOs.Insight;
using MiniRedditCloneApi.DTOs.Response;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;
using MiniRedditCloneApi.Utils;

namespace MiniRedditCloneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsightsController(IInsightsService insightsService, IEmailService emailService, IMapper mapper) : ControllerBase
    {
        private readonly IInsightsService _insightsService = insightsService;
        private readonly IEmailService _emailService = emailService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularInsights(int page = 1, int pageSize = 50)
        {
            var result = await _insightsService.GetPopularInsightsAsync(User, page, pageSize);

            if (!result.Success)
            {
                return ResultExtensions.ToErrorActionResult(result, this);
            }
            List<InsightDTO> insightDTOs = [];
            result.Data.ForEach(insight =>
            {
                var dto = _mapper.Map<InsightDTO>(insight);
                dto.IsNSFW = insight.IsNSFW || insight.IsAutoFlaggedNsfw;
                dto.Kudos = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Kudos);
                dto.Crits = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Crit);
                dto.Notes = insight.Notes.Count;
                if (insight.Media is not null)
                {
                    dto.MediaUrl = Url.Action(nameof(GetInsightMedia), "Insights", new { id = insight.Id }, Request.Scheme);

                }
                insightDTOs.Add(dto);
            });

            return Ok(insightDTOs);
        }

        [Authorize]
        [Consumes("multipart/form-data")]
        [HttpPost("herd/{herdId}")]
        public async Task<IActionResult> CreateInsight(int herdId, [FromForm] CreateInsightDTO dto)
        {
            System.Console.WriteLine(herdId);
            var result = await _insightsService.CreateInsightAsync(User, herdId, dto);

            if (!result.Success)
            {
                return ResultExtensions.ToErrorActionResult(result, this);
            }

            var insight = result.Data;
            var insightDTO = _mapper.Map<InsightDTO>(insight);
            insightDTO.IsNSFW = insight.IsNSFW || insight.IsAutoFlaggedNsfw;
            insightDTO.Kudos = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Kudos);
            insightDTO.Crits = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Crit);
            insightDTO.Notes = insight.Notes.Count;
            if (insight.Media is not null)
            {
                insightDTO.MediaUrl = Url.Action(nameof(GetInsightMedia), "Insights", new { id = insight.Id }, Request.Scheme);
            }
            return Ok(insightDTO);
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetInsightMedia(int id)
        {
            var result = await _insightsService.GetInsightMediaAsync(id);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            var contentType = Misc.GetImageContentType(result.Data);
            return File(result.Data, contentType);
        }

        [HttpGet("herd/{herdId}")]
        public async Task<IActionResult> GetHerdInsights(int herdId, int page = 1, int pageSize = 50, TopTimeRange? range = TopTimeRange.Day, InsightSort sort = InsightSort.Hot)
        {
            var result = await _insightsService.GetHerdInsightsAsync(User, herdId, sort, range, page, pageSize);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);
            
            List<InsightDTO> insightDTOs = [];
            result.Data.ForEach(insight =>
            {
                var dto = _mapper.Map<InsightDTO>(insight);
                dto.IsNSFW = insight.IsNSFW || insight.IsAutoFlaggedNsfw;
                dto.Kudos = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Kudos);
                dto.Crits = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Crit);
                dto.Notes = insight.Notes.Count;
                if (insight.Media is not null)
                {
                    dto.MediaUrl = Url.Action(nameof(GetInsightMedia), "Insights", new { id = insight.Id }, Request.Scheme);

                }
                insightDTOs.Add(dto);
            });

            return Ok(insightDTOs);
        }

        [Authorize]
        [HttpPost("{id}/kudos")]
        public async Task<IActionResult> ToggleInsightKudos(int id)
        {
            var result = await _insightsService.ToggleInsightKudosAsync(User, id);
            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpPost("{id}/crit")]
        public async Task<IActionResult> ToggleInsightCrit(int id)
        {
            var result = await _insightsService.ToggleInsightCritAsync(User, id);
            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpGet("{herdId}/pending")]
        public async Task<IActionResult> GetPendingHerdInsights(int herdId)
        {
            var result = await _insightsService.GetPendingInsightsAsync(User, herdId);
            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            List<InsightDTO> insightDTOs = [];
            result.Data.ForEach(insight =>
            {
                var dto = _mapper.Map<InsightDTO>(insight);
                dto.IsNSFW = insight.IsNSFW || insight.IsAutoFlaggedNsfw;
                dto.Kudos = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Kudos);
                dto.Crits = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Crit);
                dto.Notes = insight.Notes.Count;
                if (insight.Media is not null)
                {
                    dto.MediaUrl = Url.Action(nameof(GetInsightMedia), "Insights", new { id = insight.Id }, Request.Scheme);

                }
                insightDTOs.Add(dto);
            });

            return Ok(insightDTOs);
        }

        [Authorize]
        [HttpGet("{herdId}/removed")]
        public async Task<IActionResult> GetModeratorRemovedInsights(int herdId)
        {
            var result = await _insightsService.GetModeratorRemovedInsightsAsync(User, herdId);
            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            List<InsightDTO> insightDTOs = [];
            result.Data.ForEach(insight =>
            {
                var dto = _mapper.Map<InsightDTO>(insight);
                dto.IsNSFW = insight.IsNSFW || insight.IsAutoFlaggedNsfw;
                dto.Kudos = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Kudos);
                dto.Crits = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Crit);
                dto.Notes = insight.Notes.Count;
                if (insight.Media is not null)
                {
                    dto.MediaUrl = Url.Action(nameof(GetInsightMedia), "Insights", new { id = insight.Id }, Request.Scheme);

                }
                insightDTOs.Add(dto);
            });

            return Ok(insightDTOs);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyInsights()
        {
            var result = await _insightsService.GetMyInsightsAsync(User);
            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            List<InsightDTO> insightDTOs = [];
            result.Data.ForEach(insight =>
            {
                var dto = _mapper.Map<InsightDTO>(insight);
                dto.IsNSFW = insight.IsNSFW || insight.IsAutoFlaggedNsfw;
                dto.Kudos = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Kudos);
                dto.Crits = insight.InsightReactions.Count(reaction => reaction.Reaction == Reaction.Crit);
                dto.Notes = insight.Notes.Count;
                if (insight.Media is not null)
                {
                    dto.MediaUrl = Url.Action(nameof(GetInsightMedia), "Insights", new { id = insight.Id }, Request.Scheme);

                }
                insightDTOs.Add(dto);
            });

            return Ok(insightDTOs);
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:53 PM
# Update Fri, Jan  9, 2026  9:25:31 PM
# Update Fri, Jan  9, 2026  9:34:17 PM
// Logic update: 9ezHO4KAvANx
// Logic update: ImXC886z4amz
// Logic update: tRXrBl1S09s2
// Logic update: X6yRBYY9mAeL
// Logic update: 60RbnwBFAXcw
// Logic update: YMo3IIxfaV9F
// Logic update: KNMnuLFKJTyq
