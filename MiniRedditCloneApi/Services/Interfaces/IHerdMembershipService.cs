using System.Security.Claims;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface IHerdMembershipService
    {
        Task<Result<string>> JoinHerdAsync(ClaimsPrincipal user, int herdId);
        Task<Result<string>> LeaveHerdAsync(ClaimsPrincipal user, int herdId);
        Task<Result<int>> GetHerdNerdsCountAsync(int herdId);
    }
}
# File update Fri, Jan  9, 2026  9:16:27 PM
# Update Fri, Jan  9, 2026  9:26:11 PM
# Update Fri, Jan  9, 2026  9:35:06 PM
