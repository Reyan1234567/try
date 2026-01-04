using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data
{
    public class MiniRedditCloneApiDbContext(DbContextOptions<MiniRedditCloneApiDbContext> options) : IdentityDbContext<Nerd>(options)
    {
        public DbSet<Nerd> Nerds { get; set; }
        public DbSet<Herd> Herds { get; set; }
        public DbSet<HerdNerd> HerdNerds { get; set; }
        public DbSet<Insight> Insights { get; set; }
        public DbSet<InsightReaction> InsightReactions { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteReaction> NoteReactions { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<HerdDomain> HerdDomains { get; set; }
        public DbSet<InsightTopic> InsightTopics { get; set; }
        public DbSet<RolePromotion> RolePromotions { get; set; }
        public DbSet<RoleDemotion> RoleDemotions { get; set; }
        public DbSet<Ban> Bans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MiniRedditCloneApiDbContext).Assembly);
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:58 PM
# Update Fri, Jan  9, 2026  9:25:37 PM
# Update Fri, Jan  9, 2026  9:34:22 PM
// Logic update: g5sN3OVWHtM8
// Logic update: KhIAdJtdux9i
// Logic update: vZgmnw6T1i29
// Logic update: CUfTwC7DclqR
