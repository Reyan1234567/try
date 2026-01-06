using System.ComponentModel.DataAnnotations;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Validation;

namespace MiniRedditCloneApi.DTOs.Nerd
{
    public class NerdRegistrationDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A Username is required")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirm password fields do not match.")]
        public required string ConfirmPassword { get; set; }

        [Required]
        public ImageType AvatarType { get; set; }

        public string? DefaultAvatarNum { get; set; }

        [Image]
        [DataType(DataType.Upload)]
        public IFormFile? UploadedAvatar { get; set; }
    }
}
# File update Fri, Jan  9, 2026  9:16:02 PM
# Update Fri, Jan  9, 2026  9:25:42 PM
# Update Fri, Jan  9, 2026  9:34:28 PM
// Logic update: hNQxgc6ptEtc
// Logic update: obFXZXia23QU
// Logic update: nknDEQhB9RTY
// Logic update: vjPsgNVA3MRb
// Logic update: IzWPoE43iOSv
