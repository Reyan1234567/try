using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class PostCommentConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable("notes");

            builder.HasIndex(note => note.InsightId);
            builder.HasIndex(note => note.NerdId);

            builder.Property(note => note.CommentedAt).HasDefaultValueSql("NOW()");
            builder.Property(note => note.UpdatedAt).ValueGeneratedOnUpdate().HasDefaultValueSql("NOW()");
            builder.Property(note => note.IsEdited).HasDefaultValue(false);
            builder.Property(note => note.IsAutoFlaggedNsfw).HasDefaultValue(false);

            builder.Property(note => note.Id).HasColumnName("id");
            builder.Property(note => note.CommentedAt).HasColumnName("commented_at");
            builder.Property(note => note.UpdatedAt).HasColumnName("updated_at");
            builder.Property(note => note.Comment).HasColumnName("comment");
            builder.Property(note => note.IsEdited).HasColumnName("is_edited");
            builder.Property(note => note.IsAutoFlaggedNsfw).HasColumnName("is_autoflagged_nsfw");
            builder.Property(note => note.NsfwConfidence).HasColumnName("nsfw_confidence");
            builder.Property(note => note.InsightId).HasColumnName("insight_id");
            builder.Property(note => note.NerdId).HasColumnName("nerd_id");

        }
    }
}
# File update Fri, Jan  9, 2026  9:15:57 PM
# Update Fri, Jan  9, 2026  9:25:36 PM
# Update Fri, Jan  9, 2026  9:34:21 PM
// Logic update: khoLy2D6gALA
// Logic update: YcbV5xToS2x3
// Logic update: Ru710aoKr1z3
