using System.Security.Claims;
using MiniRedditCloneApi.DTOs.Nerd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<NerdConfirmationDTO>> RegisterNerdAsync(Nerd nerd, NerdRegistrationDTO dto);
        Task<Result<Nerd>> ConfirmNerdEmailAsync(string email, string token);
        Task<Result<Nerd>> LoginNerdAsync(NerdLoginDTO dto);
        Task<Result<string>> LogoutNerdAsync();
        Task DeleteNerdAsync();
        Task<Result<NerdConfirmationDTO>> GetPasswordResetTokenAsync(string email);
        Task<Result<string>> ResetNerdPasswordAsync(string email, string token, string newPassword);
        Task<Result<NerdConfirmationDTO>> GetChangeEmailTokenAsync(ClaimsPrincipal user, string email);
        Task<Result<Nerd>> ChangeNerdEmailAsync(string userId, string email, string token);
    }
}
# File update Fri, Jan  9, 2026  9:16:26 PM
# Update Fri, Jan  9, 2026  9:26:10 PM
# Update Fri, Jan  9, 2026  9:35:03 PM
