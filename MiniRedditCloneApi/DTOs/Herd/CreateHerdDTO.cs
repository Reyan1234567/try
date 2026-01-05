using System.ComponentModel.DataAnnotations;
using MiniRedditCloneApi.Validation;

namespace MiniRedditCloneApi.DTOs.Herd
{
    public class CreateHerdDTO
    {
        [Required(ErrorMessage = "A name is required.")]
        [MinLength(3, ErrorMessage = "The name must be at least 3 characters long.")]
        [MaxLength(20, ErrorMessage = "The name must be at most 20 characters long.")]
        public required string Name { get; set; }

        [Required]
        [MinLength(20, ErrorMessage = "The description must be at least 20 characters long.")]
        [MaxLength(150, ErrorMessage = "The description must be at most 150 characters long.")]
        public required string Description { get; set; }
        public bool AllowNSFW { get; set; }

        public List<int> DomainIds { get; set; } = [];

        [Image]
        [DataType(DataType.Upload)]
        public IFormFile? UploadedImage { get; set; }
    }
}
# File update Fri, Jan  9, 2026  9:15:59 PM
# Update Fri, Jan  9, 2026  9:25:39 PM
# Update Fri, Jan  9, 2026  9:34:23 PM
// Logic update: ZuhM3ci6QyGZ
// Logic update: IEobTUkdUDqx
// Logic update: bekBI9ZYgtQM
// Logic update: KJzzcCodjVnk
