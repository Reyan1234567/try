namespace MiniRedditCloneApi.Models
{
    public class Insight
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public required string Title { get; set; }
        public string? Text { get; set; }
        public byte[]? Media { get; set; }
        public bool IsEdited { get; set; }
        public bool IsNSFW { get; set; }
        public bool IsAutoFlaggedNsfw { get; set; }
        public float? NsfwConfidence { get; set; }

        public InsightStatus Status { get; set; } = InsightStatus.Active;

        public string? RemovedReason { get; set; }
        public DateTime? RemovedAt { get; set; }

        public string NerdId { get; set; } = null!;
        public Nerd Nerd { get; set; } = null!;

        public int HerdId { get; set; }
        public Herd Herd { get; set; } = null!;

        public ICollection<Topic> Topics { get; set; } = [];
        public ICollection<Note> Notes { get; set; } = [];
        public ICollection<InsightReaction> InsightReactions { get; set; } = [];
    }
}
# File update Fri, Jan  9, 2026  9:16:15 PM
# Update Fri, Jan  9, 2026  9:25:57 PM
# Update Fri, Jan  9, 2026  9:34:41 PM
