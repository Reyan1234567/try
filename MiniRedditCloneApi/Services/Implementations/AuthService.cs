using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MiniRedditCloneApi.DTOs.Nerd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class AuthService(UserManager<Nerd> userManager, SignInManager<Nerd> signInManager, IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly UserManager<Nerd> _userManager = userManager;
        private readonly SignInManager<Nerd> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result<NerdConfirmationDTO>> GetChangeEmailTokenAsync(ClaimsPrincipal user, string newEmail)
        {
            var nerd = await _userManager.GetUserAsync(user);

            if (nerd is null)
            {
                return Result<NerdConfirmationDTO>.Fail(ErrorType.Unauthorized, "You must be signed in.");
            }

            var existingNerd = await _userManager.FindByEmailAsync(newEmail);
            if (existingNerd is not null)
            {
                return Result<NerdConfirmationDTO>.Fail(ErrorType.Conflict, "Email is already in use.");
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(nerd, newEmail);
            return Result<NerdConfirmationDTO>.Ok(new(nerd, token));
        }

        public async Task<Result<Nerd>> ConfirmNerdEmailAsync(string email, string token)
        {
            var nerd = await _userManager.FindByEmailAsync(email);
            if (nerd is null)
            {
                return Result<Nerd>.Fail(ErrorType.NotFound, "No user found with the provided email.");
            }

            var result = await _userManager.ConfirmEmailAsync(nerd, token);
            if (!result.Succeeded)
            {
                return Result<Nerd>.Fail(
                    ErrorType.BadRequest,
                    string.Join("; ", result.Errors.Select(error => error.Description))
                );
            }

            return Result<Nerd>.Ok(nerd);
        }

        public Task DeleteNerdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Nerd>> LoginNerdAsync(NerdLoginDTO dto)
        {
            var nerd = await _userManager.FindByEmailAsync(dto.Email);

            if (nerd is not null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(nerd, dto.Password, true, false);

                if (!signInResult.Succeeded)
                {
                    var isPasswordCorrect = await _userManager.CheckPasswordAsync(nerd, dto.Password);
                    if (isPasswordCorrect)
                    {
                        var isEmailVerified = await _userManager.IsEmailConfirmedAsync(nerd);

                        if (!isEmailVerified)
                        {
                            return Result<Nerd>.Fail(ErrorType.Forbidden, "Your email is not verified.");
                        }
                    }
                    return Result<Nerd>.Fail(ErrorType.Unauthorized, "Invalid email or password.");
                }

                return Result<Nerd>.Ok(nerd);
            }
            return Result<Nerd>.Fail(ErrorType.Unauthorized, "Invalid email or password.");
        }

        public async Task<Result<string>> LogoutNerdAsync()
        {
            var nerd = _httpContextAccessor.HttpContext?.User;
            if (nerd is null || !nerd.Identity!.IsAuthenticated)
            {
                return Result<string>.Fail(ErrorType.Unauthorized, "You must be signed in.");
            }

            await _signInManager.SignOutAsync();
            return Result<string>.Ok("Logout successful.");
        }

        public async Task<Result<NerdConfirmationDTO>> RegisterNerdAsync(Nerd nerd, NerdRegistrationDTO dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null) return Result<NerdConfirmationDTO>.Fail(ErrorType.BadRequest, "Invalid request.");
            var result = await _userManager.CreateAsync(nerd, dto.Password);

            if (!result.Succeeded)
            {
                return Result<NerdConfirmationDTO>.Fail(
                    ErrorType.BadRequest,
                    string.Join("; ", result.Errors.Select(error => error.Description))
                );
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(nerd);

            return Result<NerdConfirmationDTO>.Ok(new(nerd, token));
        }

        public async Task<Result<string>> ResetNerdPasswordAsync(string email, string token, string newPassword)
        {
            var nerd = await _userManager.FindByEmailAsync(email);

            if (nerd is null)
            {
                return Result<string>.Fail(ErrorType.NotFound, "No user found with the provided email.");
            }

            var result = await _userManager.ResetPasswordAsync(nerd, token, newPassword);
            if (!result.Succeeded)
            {
                return Result<string>.Fail(
                    ErrorType.BadRequest,
                    string.Join(": ", result.Errors.Select(error => error.Description))
                );
            }

            return Result<string>.Ok("Password updated successfully.");
        }

        public async Task<Result<NerdConfirmationDTO>> GetPasswordResetTokenAsync(string email)
        {
            var nerd = await _userManager.FindByEmailAsync(email);

            if (nerd is null)
            {
                return Result<NerdConfirmationDTO>.Fail(ErrorType.NotFound, "No user found with the provided email.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(nerd);

            return Result<NerdConfirmationDTO>.Ok(new(nerd, token));
        }

        public async Task<Result<Nerd>> ChangeNerdEmailAsync(string userId, string email, string token)
        {
            var nerd = await _userManager.FindByIdAsync(userId);

            if (nerd is not null)
            {
                var result = await _userManager.ChangeEmailAsync(nerd, email, token);

                if (result.Succeeded)
                {
                    nerd = await _userManager.FindByIdAsync(userId);

                    if (nerd is not null)
                    {
                        return Result<Nerd>.Ok(nerd);
                    }
                }
                return Result<Nerd>.Fail(
                    ErrorType.BadRequest,
                    result.Errors.ToArray().Length > 0 ? string.Join("; ", result.Errors.Select(error => error.Description)) : "An error occurred, Couldn't change email."
                );
            }

            return Result<Nerd>.Fail(ErrorType.NotFound, "User not found.");

        }
    }
}
# File update Fri, Jan  9, 2026  9:16:23 PM
# Update Fri, Jan  9, 2026  9:26:06 PM
# Update Fri, Jan  9, 2026  9:34:55 PM
