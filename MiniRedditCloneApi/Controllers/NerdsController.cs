using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniRedditCloneApi.DTOs.Nerd;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Utils;

namespace MiniRedditCloneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NerdsController(INerdsService nerdsService, IMapper mapper) : ControllerBase
    {
        private readonly INerdsService _nerdsService = nerdsService;
        private readonly IMapper _mapper = mapper;

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetCurrentNerdProfile()
        {
            var result = await _nerdsService.GetNerdProfileByClaimAsync(User);

            if (!result.Success) return BadRequest(new { error = result.Error });

            var nerd = result.Data;
            var nerdDTO = _mapper.Map<NerdDTO>(nerd);

            switch (nerd?.AvatarType)
            {
                case ImageType.Default:
                    nerdDTO.AvatarUrl = $"{Request.Scheme}://{Request.Host}" + Url.Content($"~/images/avatars/avatar-{nerd.DefaultAvatarNum ?? ""}.png");
                    break;
                case ImageType.Uploaded:
                    nerdDTO.AvatarUrl = Url.Action(nameof(GetUploadedNerdAvatar), "Nerds", new { nerdId = nerd.Id }, Request.Scheme);
                    break;
                default:
                    break;
            }
            return Ok(nerdDTO);
        }

        [HttpGet("{nerdId}/profile")]
        public async Task<IActionResult> GetNerdProfile(string nerdId)
        {
            var result = await _nerdsService.GetNerdProfileByIdAsync(nerdId);

            if (!result.Success) return BadRequest(new { error = result.Error });

            var nerd = result.Data;
            var nerdDTO = _mapper.Map<NerdDTO>(nerd);

            switch (nerd?.AvatarType)
            {
                case ImageType.Default:
                    nerdDTO.AvatarUrl = Url.Content($"~/images/avatars/default-avatar-{nerd.DefaultAvatarNum ?? ""}");
                    break;
                case ImageType.Uploaded:
                    nerdDTO.AvatarUrl = Url.Action(nameof(GetUploadedNerdAvatar), "Nerds", new { nerdId = nerd.Id }, Request.Scheme);
                    break;
                default:
                    break;
            }
            return Ok(nerdDTO);
        }

        [HttpGet("{nerdId}/avatar")]
        public async Task<IActionResult> GetUploadedNerdAvatar(string nerdId)
        {
            var result = await _nerdsService.GetUploadedAvatarAsync(nerdId);

            if (!result.Success) return BadRequest(new { error = result.Error });
            if (result.Data is null) return BadRequest("An unknown error occurred.");

            var contentType = Misc.GetImageContentType(result.Data);
            return File(result.Data, contentType);
        }

        [Authorize]
        [Consumes("multipart/form-data")]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateNerdProfile([FromForm] NerdUpdateProfileDTO dto)
        {
            var result = await _nerdsService.UpdateNerdProfileAsync(User, dto);

            if (!result.Success) return BadRequest(new { error = result.Error });

            return NoContent();
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:54 PM
# Update Fri, Jan  9, 2026  9:25:32 PM
# Update Fri, Jan  9, 2026  9:34:18 PM
// Logic update: 0fJfs3SDUOWj
// Logic update: jFJV3vbZzoPT
// Logic update: k2VD3kMpOin9
// Logic update: 2RkatHzpbukY
