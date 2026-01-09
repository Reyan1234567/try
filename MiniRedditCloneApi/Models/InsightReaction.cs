namespace MiniRedditCloneApi.Models
{
    public class InsightReaction(Reaction reaction)
    {
        public int InsightId { get; set; }
        public string NerdId { get; set; } = null!;

        public DateTime ReactedAt { get; set; }
        public Reaction Reaction { get; set; } = reaction;

        public Insight Insight { get; set; } = null!;
        public Nerd Nerd { get; set; } = null!;
    }
}
# File update Fri, Jan  9, 2026  9:16:16 PM
# Update Fri, Jan  9, 2026  9:25:57 PM
# Update Fri, Jan  9, 2026  9:34:41 PM
