using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class InsightTopicConfiguration : IEntityTypeConfiguration<InsightTopic>
    {
        public void Configure(EntityTypeBuilder<InsightTopic> builder)
        {
            builder.ToTable("insight_topics");

            builder.HasKey(insightTopic => new { insightTopic.InsightId, insightTopic.TopicId });
            builder.HasIndex(insightTopic => insightTopic.TopicId);

            builder.Property(insightTopic => insightTopic.InsightId).HasColumnName("insight_id");
            builder.Property(insightTopic => insightTopic.TopicId).HasColumnName("topic_id");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:57 PM
# Update Fri, Jan  9, 2026  9:25:35 PM
# Update Fri, Jan  9, 2026  9:34:20 PM
// Logic update: C4eO46g49mCC
// Logic update: TRoGOoxNcux6
// Logic update: kXKkQTNaJ7jO
// Logic update: 7ffj9kP8tt6A
