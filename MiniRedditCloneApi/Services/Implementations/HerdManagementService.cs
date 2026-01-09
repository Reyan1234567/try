using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniRedditCloneApi.Data;
using MiniRedditCloneApi.DTOs.Herd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;
using Persic.EF.Postgres.Search;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class HerdManagementService(MiniRedditCloneApiDbContext dbContext, UserManager<Nerd> userManager) : IHerdManagementService
    {
        private readonly MiniRedditCloneApiDbContext _dbContext = dbContext;
        private readonly UserManager<Nerd> _userManager = userManager;

        public async Task<Result<List<Herd>>> GetHerdsAsync(string? q)
        {
            if (!string.IsNullOrEmpty(q))
            {
                var matchingHerds = await _dbContext.Herds.WhereSearchVectorMatches($"{q}:*").OrderBy(herd => herd.Name).Take(10).ToListAsync();

                return Result<List<Herd>>.Ok(matchingHerds);
            }

            return Result<List<Herd>>.Ok(await _dbContext.Herds.OrderBy(herd => herd.Name).Take(10).ToListAsync());
        }

        public async Task<Result<Herd>> CreateHerdAsync(CreateHerdDTO dto, ClaimsPrincipal user)
        {
            var nerd = await _userManager.GetUserAsync(user);

            if (nerd is null) return Result<Herd>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");
            var existingHerds = await _dbContext.Herds.Where(herd => herd.Name == dto.Name).ToListAsync();

            if (existingHerds.Count != 0) return Result<Herd>.Fail(ErrorType.Conflict, "This herd name is already in use.");

            var herd = new Herd
            {
                Name = dto.Name,
                Description = dto.Description,
                AllowNSFW = dto.AllowNSFW,
            };

            if (dto.UploadedImage != null && dto.UploadedImage.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.UploadedImage.CopyToAsync(ms);
                herd.Image = ms.ToArray();
            }

            if (dto.DomainIds.Count <= 0)
                return Result<Herd>.Fail(ErrorType.BadRequest, "Please include at least 1 domain.");
            var domains = await _dbContext.Domains.Where(domain => dto.DomainIds.Contains(domain.Id)).ToListAsync();

            if (domains.Count != dto.DomainIds.Count) return Result<Herd>.Fail(ErrorType.BadRequest, "Invalid domain provided.");

            using var tx = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                await _dbContext.Herds.AddAsync(herd);
                domains.ForEach(domain =>
                {
                    herd.HerdDomains.Add(
                            new()
                            {
                                DomainId = domain.Id,
                                HerdId = herd.Id,
                            }
                            );
                });

                await _dbContext.SaveChangesAsync();
                var herdNerd = new HerdNerd
                {
                    NerdId = nerd.Id,
                    HerdId = herd.Id,
                    Role = Role.Moderator
                };
                await _dbContext.HerdNerds.AddAsync(herdNerd);
                await _dbContext.SaveChangesAsync();
                await tx.CommitAsync();
                return Result<Herd>.Ok(herd);
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        public async Task<Result<Herd>> GetHerdAsync(int id)
        {
            var herd = await _dbContext.Herds.FindAsync(id);

            if (herd is null) return Result<Herd>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            return Result<Herd>.Ok(herd);
        }

        public async Task<Result<byte[]>> GetUploadedHerdImage(int id)
        {
            var herd = await _dbContext.Herds.FindAsync(id);

            if (herd is null) return Result<byte[]>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            if (herd.Image is null || herd.Image.Length <= 0)
                return Result<byte[]>.Fail(ErrorType.BadRequest, "This herd does not have an image.");

            return Result<byte[]>.Ok(herd.Image);
        }

        public async Task<Result<List<Herd>>> GetMyHerdsAsync(ClaimsPrincipal user)
        {
            var nerd = await _userManager.GetUserAsync(user);

            if (nerd is null) return Result<List<Herd>>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            return Result<List<Herd>>.Ok(await _dbContext.Herds.Where(herd => herd.HerdNerds.Select(herdNerd => herdNerd.NerdId).Contains(nerd.Id)).ToListAsync());
        }

        public async Task<Result<List<Herd>>> GetHerdsWithDomainsAsync(List<int> domainIds)
        {
            var domains = await _dbContext.Domains.Where(domain => domainIds.Contains(domain.Id)).ToListAsync();

            if (domains.Count != domainIds.Count) return Result<List<Herd>>.Fail(ErrorType.BadRequest, "Invalid domain received.");
            var herds = await _dbContext.Herds.Where(herd => domainIds.All(id => herd.HerdDomains.Select(herdDomain => herdDomain.DomainId).Contains(id))).ToListAsync();

            return Result<List<Herd>>.Ok(herds);
        }

        public async Task<Result<Herd>> UpdateHerdAsync(ClaimsPrincipal user, int herdId, UpdateHerdDTO dto)
        {
            var nerd = await _userManager.GetUserAsync(user);
            if (nerd is null) return Result<Herd>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var herd = await _dbContext.Herds.FindAsync(herdId);
            if (herd is null) return Result<Herd>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            var herdNerd = await _dbContext.HerdNerds.FindAsync(nerd.Id, herd.Id);
            if (herdNerd is null || herdNerd.Role != Role.Moderator) return Result<Herd>.Fail(ErrorType.Forbidden, "You must be a moderator to update herd.");

            var existingHerd = await _dbContext.Herds.FirstOrDefaultAsync(herd => herd.Name == dto.Name && herd.Id != herdId);
            if (existingHerd is not null) return Result<Herd>.Fail(ErrorType.Conflict, "This herd name is already in use.");

            herd.Name = dto.Name;
            herd.Description = dto.Description;
            herd.AllowNSFW = dto.AllowNSFW;
            if (dto.UploadedImage != null && dto.UploadedImage.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.UploadedImage.CopyToAsync(ms);
                herd.Image = ms.ToArray();
            }

            await _dbContext.SaveChangesAsync();
            return Result<Herd>.Ok(herd);
        }

        public async Task<Result<string?>> GetHerdRulesAsync(int herdId)
        {
            var herd = await _dbContext.Herds.FindAsync(herdId);

            if (herd is null) return Result<string?>.Fail(ErrorType.NotFound, "No herd found with the given identifier.");

            return Result<string?>.Ok(herd.Rules);
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:24 PM
# Update Fri, Jan  9, 2026  9:26:07 PM
# Update Fri, Jan  9, 2026  9:34:58 PM
