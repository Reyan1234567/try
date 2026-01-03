using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class RoleDemotionConfiguration : IEntityTypeConfiguration<RoleDemotion>
    {
        public void Configure(EntityTypeBuilder<RoleDemotion> builder)
        {
            builder.ToTable("role_demotions");

            builder.HasKey(roleDemotion => new { roleDemotion.NerdId, roleDemotion.HerdId, roleDemotion.ModeratorId });

            builder.HasOne(roleDemotion => roleDemotion.HerdNerd)
                .WithMany(herdNerd => herdNerd.RoleDemotions)
                .HasForeignKey(roleDemotion => new { roleDemotion.NerdId, roleDemotion.HerdId });

            builder.HasOne(roleDemotion => roleDemotion.Moderator)
                .WithMany(moderator => moderator.RoleDemotions)
                .HasForeignKey(roleDemotion => roleDemotion.ModeratorId);

            builder.Property(roleDemotion => roleDemotion.NerdId).HasColumnName("nerd_id");
            builder.Property(roleDemotion => roleDemotion.HerdId).HasColumnName("herd_id");
            builder.Property(roleDemotion => roleDemotion.ModeratorId).HasColumnName("moderator_id");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:58 PM
# Update Fri, Jan  9, 2026  9:25:36 PM
# Update Fri, Jan  9, 2026  9:34:21 PM
// Logic update: E9eOfeIfyoyh
// Logic update: buqZX7dcCo6v
// Logic update: jCY41PimvL2q
// Logic update: jmXedSltNv1l
