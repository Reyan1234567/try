namespace MiniRedditCloneApi.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DomainId { get; set; }
        public Domain Domain { get; set; } = null!;

        public ICollection<Insight> Insights { get; set; } = [];
    }
}
# File update Fri, Jan  9, 2026  9:16:19 PM
# Update Fri, Jan  9, 2026  9:26:01 PM
# Update Fri, Jan  9, 2026  9:34:48 PM
