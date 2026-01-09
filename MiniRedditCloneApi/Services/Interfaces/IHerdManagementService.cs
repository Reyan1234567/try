using System.Security.Claims;
using MiniRedditCloneApi.DTOs.Herd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface IHerdManagementService
    {
        Task<Result<List<Herd>>> GetHerdsAsync(string? q);
        Task<Result<Herd>> CreateHerdAsync(CreateHerdDTO dto, ClaimsPrincipal user);
        Task<Result<Herd>> GetHerdAsync(int id);
        Task<Result<byte[]>> GetUploadedHerdImage(int id);
        Task<Result<List<Herd>>> GetMyHerdsAsync(ClaimsPrincipal user);
        Task<Result<List<Herd>>> GetHerdsWithDomainsAsync(List<int> domainIds);
        Task<Result<Herd>> UpdateHerdAsync(ClaimsPrincipal user, int herdId, UpdateHerdDTO dto);
        Task<Result<string?>> GetHerdRulesAsync(int herdId);
    }
}
# File update Fri, Jan  9, 2026  9:16:27 PM
# Update Fri, Jan  9, 2026  9:26:10 PM
# Update Fri, Jan  9, 2026  9:35:05 PM
