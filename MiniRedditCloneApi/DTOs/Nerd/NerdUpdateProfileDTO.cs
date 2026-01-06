using System.ComponentModel.DataAnnotations;
using MiniRedditCloneApi.Models;
using MiniRedditCloneApi.Validation;

namespace MiniRedditCloneApi.DTOs.Nerd
{
    public class NerdUpdateProfileDTO
    {
        [Required(ErrorMessage = "A username is required.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Please choose an avatar type or leave as default")]
        public ImageType AvatarType { get; set; }

        public string? DefaultAvatarNum { get; set; }

        [Image]
        [DataType(DataType.Upload)]
        public IFormFile? UploadedAvatar { get; set; }
    }
}
# File update Fri, Jan  9, 2026  9:16:03 PM
# Update Fri, Jan  9, 2026  9:25:43 PM
# Update Fri, Jan  9, 2026  9:34:28 PM
// Logic update: LwEdHlag4VE5
// Logic update: 4mFcpUcmQy54
// Logic update: 8mCFPHM9Fova
// Logic update: 1rNpv0ALY6NV
