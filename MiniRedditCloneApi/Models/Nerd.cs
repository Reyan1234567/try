using Microsoft.AspNetCore.Identity;
using NpgsqlTypes;
using Persic.EF.Postgres.Search;

namespace MiniRedditCloneApi.Models
{
    public class Nerd : IdentityUser, IRecordWithSearchVector
    {
        public DateTime CreatedAt { get; set; }
        public NpgsqlTsVector SearchVector { get; set; } = null!;

        public ImageType AvatarType { get; set; } = ImageType.None;
        public byte[]? UploadedAvatar { get; set; }
        public string? DefaultAvatarNum { get; set; }
        public NsfwOption NsfwOption { get; set; } = NsfwOption.Blur;

        public ICollection<HerdNerd> HerdNerds { get; set; } = [];
        public ICollection<Insight> Insights { get; set; } = [];
        public ICollection<InsightReaction> InsightReactions { get; set; } = [];
        public ICollection<Note> Notes { get; set; } = [];
        public ICollection<NoteReaction> NoteReactions { get; set; } = [];
        public ICollection<RolePromotion> RolePromotions { get; set; } = [];
        public ICollection<RoleDemotion> RoleDemotions { get; set; } = [];
        public ICollection<Ban> Bans { get; set; } = [];
    }
}
# File update Fri, Jan  9, 2026  9:16:17 PM
# Update Fri, Jan  9, 2026  9:25:59 PM
# Update Fri, Jan  9, 2026  9:34:42 PM
