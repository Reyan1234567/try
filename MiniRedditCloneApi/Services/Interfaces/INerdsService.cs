using System.Security.Claims;
using MiniRedditCloneApi.DTOs.Nerd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface INerdsService
    {
        Task<Result<Nerd>> GetNerdProfileByClaimAsync(ClaimsPrincipal user);
        Task<Result<Nerd>> GetNerdProfileByIdAsync(string nerdId);
        Task<Result<byte[]>> GetUploadedAvatarAsync(string nerdId);
        Task<Result<Nerd>> UpdateNerdProfileAsync(ClaimsPrincipal user, NerdUpdateProfileDTO dto);
    }
}
# File update Fri, Jan  9, 2026  9:16:28 PM
# Update Fri, Jan  9, 2026  9:26:12 PM
# Update Fri, Jan  9, 2026  9:35:08 PM
