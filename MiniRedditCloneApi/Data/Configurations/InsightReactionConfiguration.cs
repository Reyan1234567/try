using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class PostReactionConfiguration : IEntityTypeConfiguration<InsightReaction>
    {
        public void Configure(EntityTypeBuilder<InsightReaction> builder)
        {
            builder.ToTable("insight_reactions");

            builder.HasKey(insightReaction => new { insightReaction.InsightId, insightReaction.NerdId });
            builder.HasIndex(insightReaction => new { insightReaction.InsightId, insightReaction.Reaction });

            builder.Property(insightReaction => insightReaction.ReactedAt).HasDefaultValueSql("NOW()");
            builder.Property(insightReaction => insightReaction.Reaction).HasDefaultValue(Reaction.Kudos);

            builder.Property(insightReaction => insightReaction.InsightId).HasColumnName("post_id");
            builder.Property(insightReaction => insightReaction.NerdId).HasColumnName("nerd_id");
            builder.Property(insightReaction => insightReaction.ReactedAt).HasColumnName("reacted_at");
            builder.Property(insightReaction => insightReaction.Reaction).HasColumnName("reaction");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:56 PM
# Update Fri, Jan  9, 2026  9:25:34 PM
# Update Fri, Jan  9, 2026  9:34:20 PM
// Logic update: xWynPGxHRy6x
// Logic update: wvE91PYPluev
// Logic update: oMbGTfvzEDnh
// Logic update: 2036jy4bAZtu
// Logic update: biM5XXwta3m2
