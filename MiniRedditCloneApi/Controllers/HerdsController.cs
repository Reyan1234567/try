using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniRedditCloneApi.DTOs.Domain;
using MiniRedditCloneApi.DTOs.Herd;
using MiniRedditCloneApi.DTOs.Response;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Services.Interfaces;
using MiniRedditCloneApi.Shared;
using MiniRedditCloneApi.Utils;

namespace MiniRedditCloneApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerdsController(IHerdManagementService herdManagementService, IHerdMembershipService herdMembershipService, IHerdModerationService herdModerationService, IHerdDomainService herdDomainService, IEmailService emailService, IMapper mapper) : ControllerBase
    {
        private readonly IHerdManagementService _herdManagementService = herdManagementService;
        private readonly IHerdMembershipService _herdMembershipService = herdMembershipService;
        private readonly IHerdModerationService _herdModerationService = herdModerationService;
        private readonly IHerdDomainService _herdDomainService = herdDomainService;
        private readonly IEmailService _emailService = emailService;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetHerds(SearchHerdDTO? dto)
        {
            var result = await _herdManagementService.GetHerdsAsync(dto?.Q);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);
            List<HerdDTO> herdDTOs = [];
            for (int i = 0; i < result.Data?.Count; i++)
            {
                var herd = result.Data[i];
                var herdDTO = _mapper.Map<HerdDTO>(herd);

                if (herd.Image != null && herd.Image.Length > 0)
                {
                    herdDTO.ImageUrl = Url.Action(nameof(GetHerdImage), "Herds", new { id = herd.Id }, Request.Scheme);
                }
                else
                {
                    herdDTO.ImageUrl = $"{Request.Scheme}://{Request.Host}" + Url.Content($"~/images/herd-images/default{(herd.AllowNSFW ? "-nsfw" : "")}-herd-image.png");
                }

                herdDTOs.Add(herdDTO);
            }

            return Ok(herdDTOs);
        }

        [Authorize]
        [Consumes("multipart/form-data")]
        [HttpPost]
        public async Task<IActionResult> CreateHerd([FromForm] CreateHerdDTO dto)
        {
            var result = await _herdManagementService.CreateHerdAsync(dto, User);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            var herd = result.Data;
            var herdDTO = _mapper.Map<HerdDTO>(result.Data);

            if (herd.Image != null && herd.Image.Length > 0)
            {
                herdDTO.ImageUrl = Url.Action(nameof(GetHerdImage), "Herds", new { id = herd.Id }, Request.Scheme);
            }
            else
            {
                herdDTO.ImageUrl = $"{Request.Scheme}://{Request.Host}" + Url.Content($"~/images/herd-images/default{(herd.AllowNSFW ? "-nsfw" : "")}-herd-image.png");
            }
            return CreatedAtAction(nameof(CreateHerd), new { id = result.Data.Id }, herdDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHerd(int id)
        {
            Console.WriteLine("Here");
            var result = await _herdManagementService.GetHerdAsync(id);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            var herd = result.Data;
            var herdDTO = _mapper.Map<HerdDTO>(herd);

            if (herd.Image != null && herd.Image.Length > 0)
            {
                herdDTO.ImageUrl = Url.Action(nameof(GetHerdImage), "Herds", new { id = herd.Id }, Request.Scheme);
            }
            else
            {
                herdDTO.ImageUrl = $"{Request.Scheme}://{Request.Host}" + Url.Content($"~/images/herd-images/default{(herd.AllowNSFW ? "-nsfw" : "")}-herd-image.png");
            }

            return Ok(herdDTO);
        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetHerdImage(int id)
        {
            var result = await _herdManagementService.GetUploadedHerdImage(id);
            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            if (result.Data is null) return BadRequest("An unknown error occurred.");

            var contentType = Misc.GetImageContentType(result.Data);
            return File(result.Data, contentType);
        }

        [HttpGet("{id}/count")]
        public async Task<IActionResult> GetHerdNerdCount(int id)
        {
            var result = await _herdMembershipService.GetHerdNerdsCountAsync(id);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(new { count = result.Data });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetMyHerds()
        {
            var result = await _herdManagementService.GetMyHerdsAsync(User);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            List<HerdDTO> herdDTOs = [];
            for (int i = 0; i < result.Data?.Count; i++)
            {
                var herd = result.Data[i];
                var herdDTO = _mapper.Map<HerdDTO>(herd);

                if (herd.Image != null && herd.Image.Length > 0)
                {
                    herdDTO.ImageUrl = Url.Action(nameof(GetHerdImage), "Herds", new { id = herd.Id }, Request.Scheme);
                }
                else
                {
                    herdDTO.ImageUrl = $"{Request.Scheme}://{Request.Host}" + Url.Content($"~/images/herd-images/default{(herd.AllowNSFW ? "-nsfw" : "")}-herd-image.png");
                }

                herdDTOs.Add(herdDTO);
            }

            return Ok(herdDTOs);
        }

        [HttpGet("domains")]
        public async Task<IActionResult> GetHerdsWithDomains(DomainIdListDTO dto)
        {
            var result = await _herdManagementService.GetHerdsWithDomainsAsync(dto.DomainIds);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            List<HerdDTO> herdDTOs = [];
            for (int i = 0; i < result.Data?.Count; i++)
            {
                var herd = result.Data[i];
                var herdDTO = _mapper.Map<HerdDTO>(herd);

                if (herd.Image != null && herd.Image.Length > 0)
                {
                    herdDTO.ImageUrl = Url.Action(nameof(GetHerdImage), "Herds", new { id = herd.Id }, Request.Scheme);
                }
                else
                {
                    herdDTO.ImageUrl = $"{Request.Scheme}://{Request.Host}" + Url.Content($"~/images/herd-images/default{(herd.AllowNSFW ? "-nsfw" : "")}-herd-image.png");
                }

                herdDTOs.Add(herdDTO);
            }

            return Ok(herdDTOs);
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<IActionResult> JoinHerd(int id)
        {
            var result = await _herdMembershipService.JoinHerdAsync(User, id);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpPost("{herdId}/promote/{nerdId}")]
        public async Task<IActionResult> PromoteNerdToHerdModerator(int herdId, string nerdId)
        {
            var result = await _herdModerationService.PromoteNerdToModeratorAsync(User, herdId, nerdId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpPost("{herdId}/demote/{nerdId}")]
        public async Task<IActionResult> DemoteNerdToHerdModerator(int herdId, string nerdId)
        {
            var result = await _herdModerationService.DemoteNerdFromModeratorAsync(User, herdId, nerdId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpDelete("{herdId}/resign")]
        public async Task<IActionResult> ResignFromHerdModeration(int herdId)
        {
            var result = await _herdModerationService.ResignFromHerdModerationAsync(User, herdId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpDelete("{herdId}/leave")]
        public async Task<IActionResult> LeaveHerd(int herdId)
        {
            var result = await _herdMembershipService.LeaveHerdAsync(User, herdId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [HttpGet("{herdId}/domains")]
        public async Task<IActionResult> GetHerdDomains(int herdId)
        {
            var result = await _herdDomainService.GetHerdDomainsAsync(herdId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(result.Data.Select(_mapper.Map<DomainDTO>));
        }

        [Authorize]
        [HttpPut("{herdId}/domains/{domainId}")]
        public async Task<IActionResult> AddDomainToHerd(int herdId, int domainId)
        {
            var result = await _herdDomainService.AddDomainToHerdAsync(User, herdId, domainId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpDelete("{herdId}/domains/{domainId}")]
        public async Task<IActionResult> RemoveDomainFromHerd(int herdId, int domainId)
        {
            var result = await _herdDomainService.RemoveDomainFromHerdAsync(User, herdId, domainId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<MessageDTO>(result.Data));
        }

        [Authorize]
        [HttpPut("{herdId}")]
        public async Task<IActionResult> UpdateHerd(int herdId, [FromForm] UpdateHerdDTO dto)
        {
            var result = await _herdManagementService.UpdateHerdAsync(User, herdId, dto);
            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<HerdDTO>(result.Data));
        }

        [Authorize]
        [HttpGet("{herdId}/rules")]
        public async Task<IActionResult> GetHerdRules(int herdId)
        {
            var result = await _herdManagementService.GetHerdRulesAsync(herdId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);
            return Ok(_mapper.Map<HerdRulesDTO>(result.Data));
        }

        [Authorize]
        [HttpPut("{herdId}/rules")]
        public async Task<IActionResult> UpdateHerdRules(int herdId, HerdRulesDTO dto)
        {
            var result = await _herdModerationService.UpdateHerdRulesAsync(User, herdId, dto);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            return Ok(_mapper.Map<HerdRulesDTO>(result.Data));
        }

        [Authorize]
        [HttpPost("{herdId}/ban")]
        public async Task<IActionResult> BanNerdFromHerd(int herdId, HerdBanDTO dto)
        {
            var result = await _herdModerationService.BanNerdFromHerdAsync(User, herdId, dto);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            var message = new Message([result.Data.Nerd.Email!], $"You have been banned from {result.Data.Herd.Name}", $"Dear {result.Data.Nerd.UserName}\nYou have been banned from accessing {result.Data.Herd.Name} for the following stated reason: {result.Data.Reason}. If you think this is a mistake, contact the moderators of the herd.");
            await _emailService.SendEmailAsync(message);
            return Ok(_mapper.Map<MessageDTO>("Banned user successfully."));
        }

        [Authorize]
        [HttpPost("{herdId}/unban/{nerdId}")]
        public async Task<IActionResult> UnbanNerdFromHerd(int herdId, string nerdId)
        {
            var result = await _herdModerationService.UnbanNerdFromHerdAsync(User, herdId, nerdId);

            if (!result.Success) return ResultExtensions.ToErrorActionResult(result, this);

            var message = new Message([result.Data.Nerd.Email!], $"You have been unbanned from {result.Data.Herd.Name}", $"Dear {result.Data.Nerd.UserName}\n You have been unbanned from {result.Data.Herd.Name}.");
            await _emailService.SendEmailAsync(message);
            return Ok(_mapper.Map<MessageDTO>("Unbanned user successfully."));
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:53 PM
# Update Fri, Jan  9, 2026  9:25:31 PM
# Update Fri, Jan  9, 2026  9:34:17 PM
// Logic update: ERYstmYiY90i
// Logic update: a05z2N0nJTqY
// Logic update: p8oVkBEMrDVE
// Logic update: E2zV7DG2kXHc
// Logic update: 0vDKprQhIiMZ
// Logic update: MBTBXn26m5Cy
