using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniRedditCloneApi.DTOs.Nerd;
using MiniRedditCloneApi.DTOs.Response;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;

namespace MiniRedditCloneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IMapper mapper, IEmailService emailService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;

        [Consumes("multipart/form-data")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNerd([FromForm] NerdRegistrationDTO dto)
        {
            var nerd = _mapper.Map<Nerd>(dto);

            // Avatar validation rules
            if (dto.AvatarType == ImageType.Default)
            {
                if (string.IsNullOrWhiteSpace(dto.DefaultAvatarNum)
                    || !int.TryParse(dto.DefaultAvatarNum, out int x)
                    || x <= 0
                    || x > 11
                        )
                    return BadRequest("Please choose a default avatar from the given options, or select another avatar type");

                nerd.DefaultAvatarNum = dto.DefaultAvatarNum;
                nerd.AvatarType = ImageType.Default;
            }
            else if (dto.AvatarType == ImageType.Uploaded)
            {
                if (dto.UploadedAvatar == null || dto.UploadedAvatar.Length <= 0)
                    return BadRequest("An uploaded Avatar is required when ImageType is Uploaded.");

                using var ms = new MemoryStream();
                await dto.UploadedAvatar.CopyToAsync(ms);
                nerd.UploadedAvatar = ms.ToArray();
                nerd.AvatarType = ImageType.Uploaded;
            }
            else if (dto.AvatarType == ImageType.None)
            {
                if (dto.DefaultAvatarNum != null || dto.UploadedAvatar != null)
                    return BadRequest("DefaultAvatarName and UploadedAvatar must be null when ProfileImageType is None.");

                nerd.AvatarType = ImageType.None;
            }
            var result = await _authService.RegisterNerdAsync(nerd, dto);

            if (!result.Success) return BadRequest(new { error = result.Error });

            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { token = result.Data!.Token, email = result.Data.Nerd.Email }, Request.Scheme);

            var message = new Message([result.Data.Nerd.Email!], "Finish up your NerdHerd registration", $"Dear {nerd.UserName}, You have registered successfully! Please verify your email by clicking on the provided link.\n\n{confirmationLink!}");
            await _emailService.SendEmailAsync(message);

            return CreatedAtAction(nameof(RegisterNerd), new { id = result.Data!.Nerd.Id }, _mapper.Map<NerdDTO>(result.Data.Nerd));
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignInNerd(NerdLoginDTO dto)
        {
            var result = await _authService.LoginNerdAsync(dto);

            if (!result.Success) return BadRequest(new { error = result.Error });

            return Ok(_mapper.Map<NerdDTO>(result.Data));
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutNerd()
        {
            var result = await _authService.LogoutNerdAsync();

            if (!result.Success) return BadRequest(new { error = result.Error });

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _authService.ConfirmNerdEmailAsync(email, token);
            if (!result.Success)
            {
                return BadRequest(new { error = result.Error });
            }
            return Ok(_mapper.Map<NerdDTO>(result.Data));
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> HandleForgotPasswordRequest(NerdEmailDTO dto)
        {
            var result = await _authService.GetPasswordResetTokenAsync(dto.Email);
            if (result.Success)
            {
                var resetPasswordLink = Url.Action(nameof(ResetNerdPassword), "Auth", new { email = dto.Email, token = result.Data!.Token }, Request.Scheme);
                var nerd = result.Data.Nerd;
                var message = new Message([nerd.Email!], "Password Reset Request", $"{nerd.UserName} someone has requested a password reset link for your NerdHerd account. If this wan't requested by you, you can safely ignore this message. Otherwise, you can click on the link below to reset your NerdHerd password.\n\n\n{resetPasswordLink!}");

                await _emailService.SendEmailAsync(message);
            }
            return Ok(_mapper.Map<MessageDTO>("If there's a NerdHerd account with the given email address, an email containing a password reset link will be sent to it."));
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetNerdPassword(string email, string token, string newPassword)
        {
            var result = await _authService.ResetNerdPasswordAsync(email, token, newPassword);

            if (!result.Success)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpPost("changeEmail")]
        public async Task<IActionResult> HandleChangeEmailRequest(NerdEmailDTO dto)
        {
            var result = await _authService.GetChangeEmailTokenAsync(User, dto.Email);

            if (!result.Success)
            {
                return BadRequest(new { error = result.Error });
            }

            var nerd = result.Data.Nerd;
            var changeEmailLink = Url.Action(nameof(ChangeNerdEmail), "Auth", new { email = dto.Email, userId = nerd.Id, token = result.Data.Token, }, Request.Scheme);
            var message = new Message([dto.Email], "NerdHerd account email change verification", $"Someone has requested to switch their NerdHerd account to your email. If this wasn't requested by you, you can safely ignore this message. Otherwise, click on the link below to verify the email change.\n\n{changeEmailLink}");
            await _emailService.SendEmailAsync(message);

            return Ok(_mapper.Map<MessageDTO>("If everything is in order, a verification email will be sent to the new email address you provided."));
        }

        [HttpGet("changeEmail")]
        public async Task<IActionResult> ChangeNerdEmail(string userId, string email, string token)
        {
            Console.WriteLine(email, userId, token);
            Result<Nerd> result = await _authService.ChangeNerdEmailAsync(userId, email, token);

            if (!result.Success)
            {
                return BadRequest(new { error = result.Error });
            }
            return Ok(_mapper.Map<NerdDTO>(result.Data));
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:53 PM
# Update Fri, Jan  9, 2026  9:25:31 PM
# Update Fri, Jan  9, 2026  9:34:17 PM
// Logic update: dBX6XLyEpeEh
// Logic update: 0Zd30h03YvTb
// Logic update: P2290y9oy3E0
// Logic update: 8bl9CLAP2Tfd
// Logic update: W6uVk1uULcv0
