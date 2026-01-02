using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Insight>
    {
        public void Configure(EntityTypeBuilder<Insight> builder)
        {
            builder.ToTable("insights");

            builder.HasIndex(insight => insight.HerdId);
            builder.HasIndex(insight => insight.NerdId);

            builder.Property(insight => insight.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(insight => insight.UpdatedAt).ValueGeneratedOnUpdate().HasDefaultValueSql("NOW()");
            builder.Property(insight => insight.IsEdited).HasDefaultValue(false);
            builder.Property(insight => insight.IsNSFW).HasDefaultValue(false);
            builder.Property(insight => insight.IsAutoFlaggedNsfw).HasDefaultValue(false);

            builder.Property(insight => insight.Id).HasColumnName("id");
            builder.Property(insight => insight.CreatedAt).HasColumnName("created_at");
            builder.Property(insight => insight.UpdatedAt).HasColumnName("updated_at");
            builder.Property(insight => insight.Title).HasColumnName("title");
            builder.Property(insight => insight.Text).HasColumnName("text");
            builder.Property(insight => insight.Media).HasColumnName("media");
            builder.Property(insight => insight.IsEdited).HasColumnName("is_edited");
            builder.Property(insight => insight.IsNSFW).HasColumnName("is_nsfw");
            builder.Property(insight => insight.IsAutoFlaggedNsfw).HasColumnName("is_auto_flagged_nsfw");
            builder.Property(insight => insight.NsfwConfidence).HasColumnName("nsfw_confidence");
            builder.Property(insight => insight.Status).HasColumnName("status");
            builder.Property(insight => insight.RemovedReason).HasColumnName("removed_reason");
            builder.Property(insight => insight.RemovedAt).HasColumnName("removed_at");
            builder.Property(insight => insight.NerdId).HasColumnName("nerd_id");
            builder.Property(insight => insight.HerdId).HasColumnName("herd_id");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:56 PM
# Update Fri, Jan  9, 2026  9:25:34 PM
# Update Fri, Jan  9, 2026  9:34:20 PM
// Logic update: cUHIC7AkQTTx
// Logic update: wlSMoZ7zSvr6
// Logic update: eSTjuiI9sZZ9
// Logic update: i0tLBBbyOvp6
// Logic update: 5ocd9iy1vsad
// Logic update: HpW6kmwpllAy
// Logic update: OT76BPKX5HzW
