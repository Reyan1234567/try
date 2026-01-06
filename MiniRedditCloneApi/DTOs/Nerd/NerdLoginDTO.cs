using System.ComponentModel.DataAnnotations;

namespace MiniRedditCloneApi.DTOs.Nerd
{
    public class NerdLoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
# File update Fri, Jan  9, 2026  9:16:02 PM
# Update Fri, Jan  9, 2026  9:25:42 PM
# Update Fri, Jan  9, 2026  9:34:27 PM
// Logic update: BM1Ss7lr91PB
// Logic update: y6F7GJF6KpOO
// Logic update: SUg1lQg8vBTV
