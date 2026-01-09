using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedditCloneApi.Data;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class HerdMembershipService(MiniRedditCloneApiDbContext dbContext, UserManager<Nerd> userManager) : IHerdMembershipService
    {
        private readonly MiniRedditCloneApiDbContext _dbContext = dbContext;
        private readonly UserManager<Nerd> _userManager = userManager;

        public async Task<Result<string>> JoinHerdAsync(ClaimsPrincipal user, int herdId)
        {
            var nerd = await _userManager.GetUserAsync(user);

            if (nerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);

            if (herd is null) return Result<string>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var ban = await _dbContext.Bans.FindAsync(nerd.Id, herd.Id);
            if (ban is not null) return Result<string>.Fail(ErrorType.Forbidden, $"You have been banned from the herd with the following reason: [{ban.Reason}].");

            var existingHerdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);

            if (existingHerdNerd is null)
            {
                var herdNerd = new HerdNerd
                {
                    NerdId = nerd.Id,
                    HerdId = herd.Id,
                    Role = Role.Member
                };

                await _dbContext.HerdNerds.AddAsync(herdNerd);
                await _dbContext.SaveChangesAsync();
            }

            return Result<string>.Ok("Joined herd successfully.");
        }

        public async Task<Result<string>> LeaveHerdAsync(ClaimsPrincipal user, int herdId)
        {
            var nerd = await _userManager.GetUserAsync(user);

            if (nerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);

            if (herd is null) return Result<string>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);

            if (herdNerd is null) return Result<string>.Fail(ErrorType.Forbidden, "You must be a herd member to leave.");

            using var tx = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                _dbContext.HerdNerds.Remove(herdNerd);
                await _dbContext.SaveChangesAsync();
                var members = await _dbContext.HerdNerds.Where(herdNerd => herdNerd.HerdId == herd.Id).OrderBy(herdNerd => herdNerd.JoinedAt).ToListAsync();
                var moderators = await _dbContext.HerdNerds.Where(herdNerd => herdNerd.HerdId == herd.Id && herdNerd.Role == Role.Moderator).ToListAsync();

                if (members.Count == 0)
                {
                    _dbContext.Herds.Remove(herd);
                }
                else if (moderators.Count == 0)
                {
                    members[0].Role = Role.Moderator;
                }

                await _dbContext.SaveChangesAsync();

                await tx.CommitAsync();
                return Result<string>.Ok("Left herd successfully.");
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<Result<int>> GetHerdNerdsCountAsync(int herdId)
        {
            var herd = await _dbContext.Herds.FindAsync(herdId);

            if (herd is null) return Result<int>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerdsCount = (await _dbContext.HerdNerds.Where(herdNerd => herdNerd.HerdId == herdId).ToListAsync()).Count;
            return Result<int>.Ok(herdNerdsCount);
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:24 PM
# Update Fri, Jan  9, 2026  9:26:08 PM
# Update Fri, Jan  9, 2026  9:34:59 PM
