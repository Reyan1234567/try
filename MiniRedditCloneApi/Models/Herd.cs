using NpgsqlTypes;
using Persic.EF.Postgres.Search;

namespace MiniRedditCloneApi.Models
{
    public class Herd : IRecordWithSearchVector
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public required string Name { get; set; }
        public NpgsqlTsVector SearchVector { get; set; } = null!;
        public required string Description { get; set; }
        public string? Rules { get; set; }
        public bool AllowNSFW { get; set; } = false;
        public byte[]? Image { get; set; }

        public ICollection<HerdNerd> HerdNerds { get; set; } = [];
        public ICollection<HerdDomain> HerdDomains { get; set; } = [];
        public ICollection<Ban> Bans { get; set; } = [];
        public ICollection<Insight> Insights { get; set; } = [];
    }
}
# File update Fri, Jan  9, 2026  9:16:14 PM
# Update Fri, Jan  9, 2026  9:25:54 PM
# Update Fri, Jan  9, 2026  9:34:39 PM
