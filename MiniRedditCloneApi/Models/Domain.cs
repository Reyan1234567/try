namespace MiniRedditCloneApi.Models
{
    public class Domain
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // For future flexibility
        public bool IsSystem { get; set; } = true;

        public ICollection<Topic> Topics { get; set; } = [];
        public ICollection<HerdDomain> HerdDomains { get; set; } = [];
    }
}
# File update Fri, Jan  9, 2026  9:16:13 PM
# Update Fri, Jan  9, 2026  9:25:54 PM
# Update Fri, Jan  9, 2026  9:34:39 PM
