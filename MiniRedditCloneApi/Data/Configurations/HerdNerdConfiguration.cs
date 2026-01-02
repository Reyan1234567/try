using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class HerdNerdConfiguration : IEntityTypeConfiguration<HerdNerd>
    {
        public void Configure(EntityTypeBuilder<HerdNerd> builder)
        {
            builder.ToTable("herd_nerds");

            builder.HasKey(herdNerd => new { herdNerd.NerdId, herdNerd.HerdId });
            builder.HasIndex(herdNerd => new { herdNerd.HerdId, herdNerd.Role });

            builder.Property(herdNerd => herdNerd.JoinedAt).HasDefaultValueSql("NOW()");
            builder.Property(herdNerd => herdNerd.Role).HasDefaultValue(Role.Member);

            builder.Property(herdNerd => herdNerd.NerdId).HasColumnName("nerd_id");
            builder.Property(herdNerd => herdNerd.HerdId).HasColumnName("herd_id");
            builder.Property(herdNerd => herdNerd.JoinedAt).HasColumnName("joined_at");
            builder.Property(herdNerd => herdNerd.Role).HasColumnName("role");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:56 PM
# Update Fri, Jan  9, 2026  9:25:34 PM
# Update Fri, Jan  9, 2026  9:34:20 PM
// Logic update: sFX8MuamkalA
// Logic update: N84mgZV0LSJ3
// Logic update: FcQDmeQWm7ff
// Logic update: CRonCyCU1mLR
// Logic update: UFVLXuq5NW1G
// Logic update: LdE2xdxETdAa
