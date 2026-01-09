using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedditCloneApi.Data;
using MiniRedditCloneApi.DTOs.Herd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class HerdModerationService(MiniRedditCloneApiDbContext dbContext, UserManager<Nerd> userManager) : IHerdModerationService
    {
        private readonly MiniRedditCloneApiDbContext _dbContext = dbContext;
        private readonly UserManager<Nerd> _userManager = userManager;

        public async Task<Result<string>> PromoteNerdToModeratorAsync(ClaimsPrincipal user, int herdId, string nerdId)
        {
            var modNerd = await _userManager.GetUserAsync(user);
            if (modNerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<string>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var modHerdNerd = await _dbContext.HerdNerds.FindAsync(modNerd.Id, herd.Id);
            if (modHerdNerd is null || modHerdNerd.Role != Role.Moderator) return Result<string>.Fail(ErrorType.Forbidden, "You must be a moderator to promote members.");

            var promotedNerd = await _userManager.FindByIdAsync(nerdId);
            var promotedHerdNerd = await _dbContext.HerdNerds.FindAsync(promotedNerd?.Id, herd.Id);
            if (promotedNerd is null || promotedHerdNerd is null) return Result<string>.Fail(ErrorType.NotFound, "The user must exist and be a herd member.");

            if (promotedHerdNerd.Role != Role.Member) return Result<string>.Fail(ErrorType.BadRequest, "The user is already a moderator.");

            var existingPromotion = await _dbContext.RolePromotions.FindAsync(promotedNerd.Id, herd.Id, modNerd.Id);
            if (existingPromotion is not null) return Result<string>.Fail(ErrorType.Conflict, "You have already promoted this user.");

            using var tx = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var rolePromotion = new RolePromotion
                {
                    NerdId = promotedHerdNerd.NerdId,
                    HerdId = promotedHerdNerd.HerdId,
                    ModeratorId = modNerd.Id
                };
                await _dbContext.RolePromotions.AddAsync(rolePromotion);
                await _dbContext.SaveChangesAsync();

                var modsCount = (await _dbContext.HerdNerds.Where(herdNerd => herdNerd.HerdId == herd.Id && herdNerd.Role == Role.Moderator).ToListAsync()).Count;
                var nerdPromotionsCount = (await _dbContext.RolePromotions.Where(rolePromotion => rolePromotion.NerdId == promotedNerd.Id && rolePromotion.HerdId == herd.Id).ToListAsync()).Count;

                if (nerdPromotionsCount >= modsCount / 2)
                {
                    promotedHerdNerd.Role = Role.Moderator;
                    var demotions = await _dbContext.RoleDemotions.Where(roleDemotion => roleDemotion.NerdId == promotedNerd.Id && roleDemotion.HerdId == herd.Id).ToListAsync();
                    _dbContext.RoleDemotions.RemoveRange(demotions);
                    await _dbContext.SaveChangesAsync();
                }
                await tx.CommitAsync();
                return Result<string>.Ok("User promoted successfully.");
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<Result<string>> DemoteNerdFromModeratorAsync(ClaimsPrincipal user, int herdId, string nerdId)
        {
            var modNerd = await _userManager.GetUserAsync(user);
            if (modNerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<string>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var modHerdNerd = await _dbContext.HerdNerds.FindAsync(modNerd.Id, herd.Id);
            if (modHerdNerd is null || modHerdNerd.Role != Role.Moderator) return Result<string>.Fail(ErrorType.Forbidden, "You must be a moderator to demote members.");

            var demotedNerd = await _userManager.FindByIdAsync(nerdId);
            if (demotedNerd is null) return Result<string>.Fail(ErrorType.NotFound, "The user must exist and be a herd moderator.");

            var demotedHerdNerd = await _dbContext.HerdNerds.FindAsync(demotedNerd.Id, herd.Id);
            if (demotedHerdNerd is null || demotedHerdNerd.Role != Role.Moderator) return Result<string>.Fail(ErrorType.BadRequest, "The user is not a moderator");

            var existingDemotion = await _dbContext.RoleDemotions.FindAsync(demotedNerd.Id, herd.Id, modNerd.Id);
            if (existingDemotion is not null) return Result<string>.Fail(ErrorType.Conflict, "You have already demoted this user.");

            using var tx = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var roleDemotion = new RoleDemotion
                {
                    NerdId = demotedHerdNerd.NerdId,
                    HerdId = demotedHerdNerd.HerdId,
                    ModeratorId = modNerd.Id
                };
                await _dbContext.RoleDemotions.AddAsync(roleDemotion);
                await _dbContext.SaveChangesAsync();

                var modsCount = (await _dbContext.HerdNerds.Where(herdNerd => herdNerd.HerdId == herd.Id && herdNerd.Role == Role.Moderator).ToListAsync()).Count;
                var nerdDemotionsCount = (await _dbContext.RoleDemotions.Where(roleDemotion => roleDemotion.NerdId == demotedNerd.Id && roleDemotion.HerdId == herd.Id).ToListAsync()).Count;

                if (nerdDemotionsCount >= modsCount / 2)
                {
                    demotedHerdNerd.Role = Role.Member;
                    var promotions = await _dbContext.RolePromotions.Where(rolePromotion => rolePromotion.NerdId == demotedNerd.Id && rolePromotion.HerdId == herd.Id).ToListAsync();
                    _dbContext.RolePromotions.RemoveRange(promotions);
                    await _dbContext.SaveChangesAsync();
                }

                await tx.CommitAsync();
                return Result<string>.Ok("User demoted successfully.");
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<Result<string>> ResignFromHerdModerationAsync(ClaimsPrincipal user, int herdId)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<string>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator) return Result<string>.Fail(ErrorType.Forbidden, "You must be a moderator to resign.");

            var modsCount = (await _dbContext.HerdNerds.Where(herdNerd => herdNerd.HerdId == herd.Id && herdNerd.Role == Role.Moderator).ToListAsync()).Count;

            if (modsCount <= 1)
            {
                return Result<string>.Fail(ErrorType.BadRequest, "There must be at least one other moderator before resigning.");
            }

            using var tx = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                herdNerd.Role = Role.Member;
                var promotions = await _dbContext.RolePromotions.Where(rolePromotion => rolePromotion.ModeratorId == nerd.Id).ToListAsync();
                var demotions = await _dbContext.RoleDemotions.Where(roleDemotion => roleDemotion.ModeratorId == nerd.Id).ToListAsync();
                _dbContext.RolePromotions.RemoveRange(promotions);
                _dbContext.RoleDemotions.RemoveRange(demotions);
                await _dbContext.SaveChangesAsync();

                await tx.CommitAsync();
                return Result<string>.Ok("You have resigned as moderator.");
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<Result<string?>> UpdateHerdRulesAsync(ClaimsPrincipal user, int herdId, HerdRulesDTO dto)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<string?>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<string?>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator) return Result<string?>.Fail(ErrorType.Forbidden, "You must be a moderator to update herd rules.");

            herd.Rules = dto.Rules;
            await _dbContext.SaveChangesAsync();

            return Result<string?>.Ok(herd.Rules);
        }

        public async Task<Result<Ban>> BanNerdFromHerdAsync(ClaimsPrincipal user, int herdId, HerdBanDTO dto)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<Ban>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<Ban>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator) return Result<Ban>.Fail(ErrorType.Forbidden, "You must be a moderator to ban users.");

            var bannedNerd = await _userManager.FindByIdAsync(dto.NerdId);
            if (bannedNerd is null) return Result<Ban>.Fail(ErrorType.NotFound, "No nerd found with the given identifier.");

            if (nerd.Id == bannedNerd.Id) return Result<Ban>.Fail(ErrorType.BadRequest, "You cannot ban yourself.");
            var bannedHerdNerd = await _dbContext.HerdNerds.FindAsync(bannedNerd.Id, herd.Id);
            if (bannedHerdNerd is null || bannedHerdNerd.Role != Role.Member) return Result<Ban>.Fail(ErrorType.BadRequest, "The banned user must be a member of the herd.");

            using var tx = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.HerdNerds.Remove(bannedHerdNerd);
                var ban = new Ban()
                {
                    NerdId = bannedNerd.Id,
                    HerdId = herd.Id,
                    Reason = dto.Reason
                };
                await _dbContext.Bans.AddAsync(ban);
                await _dbContext.SaveChangesAsync();
                await tx.CommitAsync();
                return Result<Ban>.Ok(ban);
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<Result<Ban>> UnbanNerdFromHerdAsync(ClaimsPrincipal user, int herdId, string unbannedNerdId)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<Ban>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<Ban>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator) return Result<Ban>.Fail(ErrorType.Forbidden, "You must be a moderator to unban users.");

            var unbannedNerd = await _userManager.FindByIdAsync(unbannedNerdId);
            if (unbannedNerd is null) return Result<Ban>.Fail(ErrorType.NotFound, "No nerd found with the given identifier.");

            if (nerd.Id == unbannedNerd.Id) return Result<Ban>.Fail(ErrorType.BadRequest, "You cannot unban yourself.");

            var ban = await _dbContext.Bans.FindAsync(unbannedNerd.Id, herd.Id);
            if (ban is null) return Result<Ban>.Fail(ErrorType.BadRequest, "This user is not banned.");

            _dbContext.Bans.Remove(ban);
            await _dbContext.SaveChangesAsync();
            return Result<Ban>.Ok(ban);
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:24 PM
# Update Fri, Jan  9, 2026  9:26:08 PM
# Update Fri, Jan  9, 2026  9:34:59 PM
