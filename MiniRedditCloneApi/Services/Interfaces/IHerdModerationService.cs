using System.Security.Claims;
using MiniRedditCloneApi.DTOs.Herd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface IHerdModerationService
    {
        Task<Result<string>> PromoteNerdToModeratorAsync(ClaimsPrincipal user, int herdId, string nerdId);
        Task<Result<string>> DemoteNerdFromModeratorAsync(ClaimsPrincipal user, int herdId, string nerdId);
        Task<Result<string>> ResignFromHerdModerationAsync(ClaimsPrincipal user, int herdId);
        Task<Result<string?>> UpdateHerdRulesAsync(ClaimsPrincipal user, int herdId, HerdRulesDTO dto);
        Task<Result<Ban>> BanNerdFromHerdAsync(ClaimsPrincipal user, int herdId, HerdBanDTO dto);
        Task<Result<Ban>> UnbanNerdFromHerdAsync(ClaimsPrincipal user, int herdId, string unbannedNerdId);
    }
}
# File update Fri, Jan  9, 2026  9:16:28 PM
# Update Fri, Jan  9, 2026  9:26:11 PM
# Update Fri, Jan  9, 2026  9:35:07 PM
