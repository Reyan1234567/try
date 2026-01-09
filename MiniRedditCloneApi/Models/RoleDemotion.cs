namespace MiniRedditCloneApi.Models
{
    public class RoleDemotion
    {
        public required string NerdId { get; set; }
        public int HerdId { get; set; }
        public required string ModeratorId { get; set; }

        public HerdNerd HerdNerd { get; set; } = null!;
        public Nerd Moderator { get; set; } = null!;
    }
}
# File update Fri, Jan  9, 2026  9:16:18 PM
# Update Fri, Jan  9, 2026  9:26:01 PM
# Update Fri, Jan  9, 2026  9:34:47 PM
