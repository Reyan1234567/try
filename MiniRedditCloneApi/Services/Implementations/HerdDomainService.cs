using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedditCloneApi.Data;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class HerdDomainService(MiniRedditCloneApiDbContext dbContext, UserManager<Nerd> userManager) : IHerdDomainService
    {
        private readonly MiniRedditCloneApiDbContext _dbContext = dbContext;
        private readonly UserManager<Nerd> _userManager = userManager;

        public async Task<Result<List<Domain>>> GetHerdDomainsAsync(int herdId)
        {
            var herd = await _dbContext.Herds
                .Include(herd => herd.HerdDomains)
                .FirstOrDefaultAsync(herd => herd.Id == herdId);
            if (herd is null) return Result<List<Domain>>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdDomainIds = herd.HerdDomains.Select(herdDomain => herdDomain.DomainId);
            return Result<List<Domain>>.Ok(await _dbContext.Domains.Where(domain => herdDomainIds.Contains(domain.Id)).ToListAsync());
        }

        public async Task<Result<string>> AddDomainToHerdAsync(ClaimsPrincipal user, int herdId, int domainId)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.Include(herd => herd.HerdDomains).FirstOrDefaultAsync(herd => herd.Id == herdId);
            if (herd is null) return Result<string>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator) return Result<string>.Fail(ErrorType.Forbidden, "You must be a moderator to modify domains.");

            var domain = await _dbContext.Domains.FindAsync(domainId);
            if (domain is null) return Result<string>.Fail(ErrorType.NotFound, "No domain found with the given identifier.");

            if (!herd.HerdDomains.Select(herdDomain => herdDomain.DomainId).Contains(domain.Id))
            {
                using var tx = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var herdDomain = new HerdDomain
                    {
                        DomainId = domain.Id,
                        HerdId = herd.Id
                    };
                    herd.HerdDomains.Add(herdDomain);
                    await _dbContext.SaveChangesAsync();

                    await tx.CommitAsync();
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            }
            return Result<string>.Ok("Domain added succesfully.");
        }

        public async Task<Result<string>> RemoveDomainFromHerdAsync(ClaimsPrincipal user, int herdId, int domainId)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<string>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.Include(herd => herd.HerdDomains).FirstOrDefaultAsync(herd => herd.Id == herdId);
            if (herd is null) return Result<string>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator) return Result<string>.Fail(ErrorType.Forbidden, "You must be a moderator to modify domains.");

            if (herd.HerdDomains.Count <= 1) return Result<string>.Fail(ErrorType.BadRequest, "The herd nust have at least one domain remaining.");

            var domain = await _dbContext.Domains.FindAsync(domainId);
            if (domain is null) return Result<string>.Fail(ErrorType.NotFound, "No domain found with the given identifier.");
            var herdDomain = herd.HerdDomains.FirstOrDefault(herdDomain => herdDomain.DomainId == domain.Id);

            if (herdDomain is null) return Result<string>.Fail(ErrorType.BadRequest, "The herd does not contain the specified domain.");

            using var tx = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                herd.HerdDomains.Remove(herdDomain);
                await _dbContext.SaveChangesAsync();

                await tx.CommitAsync();
                return Result<string>.Ok("Domain removed successfully.");
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:23 PM
# Update Fri, Jan  9, 2026  9:26:07 PM
# Update Fri, Jan  9, 2026  9:34:57 PM
