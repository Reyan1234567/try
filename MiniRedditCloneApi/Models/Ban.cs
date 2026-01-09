namespace MiniRedditCloneApi.Models
{
    public class Ban
    {
        public required string NerdId { get; set; }
        public int HerdId { get; set; }

        public required string Reason { get; set; }
        public DateTime BannedAt { get; set; }

        public Nerd Nerd { get; set; } = null!;
        public Herd Herd { get; set; } = null!;
    }
}
# File update Fri, Jan  9, 2026  9:16:13 PM
# Update Fri, Jan  9, 2026  9:25:54 PM
# Update Fri, Jan  9, 2026  9:34:38 PM
