namespace MiniRedditCloneApi.Models
{
    public class Note(string comment)
    {
        public int Id { get; set; }
        public DateTime CommentedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Comment { get; set; } = comment;
        public bool IsEdited { get; set; }
        public bool IsAutoFlaggedNsfw { get; set; }
        public float? NsfwConfidence { get; set; }


        public int InsightId { get; set; }
        public Insight Insight { get; set; } = null!;

        public string NerdId { get; set; } = null!;
        public Nerd Nerd { get; set; } = null!;

        public ICollection<NoteReaction> NoteReactions { get; set; } = [];
    }
}
# File update Fri, Jan  9, 2026  9:16:17 PM
# Update Fri, Jan  9, 2026  9:25:59 PM
# Update Fri, Jan  9, 2026  9:34:43 PM
