using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MiniRedditCloneApi.DTOs.Nerd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Services.Implementations
{
    public class NerdsService(UserManager<Nerd> userManager) : INerdsService
    {
        private readonly UserManager<Nerd> _userManager = userManager;

        public async Task<Result<byte[]>> GetUploadedAvatarAsync(string nerdId)
        {
            var nerd = await _userManager.FindByIdAsync(nerdId);

            if (nerd is null)
                return Result<byte[]>.Fail(ErrorType.NotFound, "No user found with the given identifier.");

            if (nerd.AvatarType != ImageType.Uploaded || nerd.UploadedAvatar is null)
                return Result<byte[]>.Fail(ErrorType.BadRequest, "This user does not have an uploaded avatar.");

            return Result<byte[]>.Ok(nerd.UploadedAvatar);
        }

        public async Task<Result<Nerd>> GetNerdProfileByClaimAsync(ClaimsPrincipal user)
        {
            var identityNerd = await _userManager.GetUserAsync(user);

            if (identityNerd is null)
                return Result<Nerd>.Fail(ErrorType.Unauthorized, "You must be signed in.");

            return Result<Nerd>.Ok(identityNerd);
        }

        public async Task<Result<Nerd>> GetNerdProfileByIdAsync(string nerdId)
        {
            var nerd = await _userManager.FindByIdAsync(nerdId);

            if (nerd is null)
                return Result<Nerd>.Fail(ErrorType.NotFound, "No user found with the given identifier.");

            return Result<Nerd>.Ok(nerd);
        }

        public async Task<Result<Nerd>> UpdateNerdProfileAsync(ClaimsPrincipal user, NerdUpdateProfileDTO dto)
        {
            var identityNerd = await _userManager.GetUserAsync(user);

            if (identityNerd is null)
                return Result<Nerd>.Fail(ErrorType.Unauthorized, "Please sign in to complete this action.");

            var existingUsername = await _userManager.FindByNameAsync(dto.Username);
            if (existingUsername is not null && existingUsername != identityNerd)
                return Result<Nerd>.Fail(ErrorType.Conflict, "This username is already taken.");

            identityNerd.UserName = dto.Username;

            switch (dto.AvatarType)
            {
                case ImageType.Uploaded:
                    if (dto.UploadedAvatar == null || dto.UploadedAvatar.Length <= 0)
                    {
                        if (identityNerd.AvatarType != ImageType.Uploaded || identityNerd.UploadedAvatar == null)
                            return Result<Nerd>.Fail(ErrorType.BadRequest, "An uploaded avatar is required.");
                    }
                    else
                    {
                        using var ms = new MemoryStream();
                        await dto.UploadedAvatar.CopyToAsync(ms);
                        identityNerd.UploadedAvatar = ms.ToArray();
                        identityNerd.AvatarType = ImageType.Uploaded;
                    }
                    break;

                case ImageType.Default:
                    if (dto.DefaultAvatarNum == null && (identityNerd.AvatarType != ImageType.Default || identityNerd.DefaultAvatarNum == null))
                        return Result<Nerd>.Fail(ErrorType.BadRequest, "Please select a valid default avatar.");

                    identityNerd.DefaultAvatarNum = dto.DefaultAvatarNum;
                    identityNerd.UploadedAvatar = null;
                    identityNerd.AvatarType = ImageType.Default;
                    break;

                default:
                    if (dto.DefaultAvatarNum != null || dto.UploadedAvatar != null)
                        return Result<Nerd>.Fail(ErrorType.BadRequest, "Avatar fields must be null when selecting 'None'.");

                    identityNerd.DefaultAvatarNum = null;
                    identityNerd.UploadedAvatar = null;
                    identityNerd.AvatarType = ImageType.None;
                    break;
            }

            var result = await _userManager.UpdateAsync(identityNerd);
            if (!result.Succeeded)
                return Result<Nerd>.Fail(ErrorType.BadRequest, string.Join("; ", result.Errors.Select(e => e.Description)));

            return Result<Nerd>.Ok(identityNerd);
        }
    }
}
# File update Fri, Jan  9, 2026  9:16:25 PM
# Update Fri, Jan  9, 2026  9:26:09 PM
# Update Fri, Jan  9, 2026  9:35:01 PM
