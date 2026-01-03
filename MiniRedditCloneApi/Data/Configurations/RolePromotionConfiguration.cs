using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class RolePromotionConfiguration : IEntityTypeConfiguration<RolePromotion>
    {
        public void Configure(EntityTypeBuilder<RolePromotion> builder)
        {
            builder.ToTable("role_promotions");

            builder.HasKey(rolePromotion => new { rolePromotion.NerdId, rolePromotion.HerdId, rolePromotion.ModeratorId });

            builder.HasOne(rolePromotion => rolePromotion.HerdNerd)
                .WithMany(herdNerd => herdNerd.RolePromotions)
                .HasForeignKey(rolePromotion => new { rolePromotion.NerdId, rolePromotion.HerdId });

            builder.HasOne(rolePromotion => rolePromotion.Moderator)
                .WithMany(moderator => moderator.RolePromotions)
                .HasForeignKey(rolePromotion => rolePromotion.ModeratorId);

            builder.Property(rolePromotion => rolePromotion.NerdId).HasColumnName("nerd_id");
            builder.Property(rolePromotion => rolePromotion.HerdId).HasColumnName("herd_id");
            builder.Property(rolePromotion => rolePromotion.ModeratorId).HasColumnName("moderator_id");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:58 PM
# Update Fri, Jan  9, 2026  9:25:37 PM
# Update Fri, Jan  9, 2026  9:34:22 PM
// Logic update: jAmd3O1UYvFb
// Logic update: fWG55yNRszZ0
// Logic update: iyLFSrOItVy2
// Logic update: gSmcPtX5ih58
// Logic update: ZqmghLUklYRc
// Logic update: sg5FmChnaUDq
