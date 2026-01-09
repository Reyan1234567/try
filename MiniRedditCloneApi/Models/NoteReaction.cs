namespace MiniRedditCloneApi.Models
{
    public class NoteReaction(Reaction reaction)
    {
        public int NoteId { get; set; }
        public string NerdId { get; set; } = null!;

        public DateTime ReactedAt { get; set; }
        public Reaction Reaction { get; set; } = reaction;

        public Note Note { get; set; } = null!;
        public Nerd Nerd { get; set; } = null!;
    }
}
# File update Fri, Jan  9, 2026  9:16:17 PM
# Update Fri, Jan  9, 2026  9:25:59 PM
# Update Fri, Jan  9, 2026  9:34:43 PM
