namespace MiniRedditCloneApi.Models
{
    public class HerdNerd(Role role = Role.Member)
    {
        public string NerdId { get; set; } = null!;
        public int HerdId { get; set; }

        public DateTime JoinedAt { get; set; }
        public Role Role { get; set; } = role;

        public Nerd Nerd { get; set; } = null!;
        public Herd Herd { get; set; } = null!;
        public ICollection<RolePromotion> RolePromotions { get; set; } = [];
        public ICollection<RoleDemotion> RoleDemotions { get; set; } = [];
    }
}
# File update Fri, Jan  9, 2026  9:16:14 PM
# Update Fri, Jan  9, 2026  9:25:56 PM
# Update Fri, Jan  9, 2026  9:34:40 PM
