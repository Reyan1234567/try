using System.Security.Claims;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface IHerdDomainService
    {
        Task<Result<List<Domain>>> GetHerdDomainsAsync(int herdId);
        Task<Result<string>> AddDomainToHerdAsync(ClaimsPrincipal user, int herdId, int domainId);
        Task<Result<string>> RemoveDomainFromHerdAsync(ClaimsPrincipal user, int herdId, int domainId);
    }
}
# File update Fri, Jan  9, 2026  9:16:27 PM
# Update Fri, Jan  9, 2026  9:26:10 PM
# Update Fri, Jan  9, 2026  9:35:04 PM
