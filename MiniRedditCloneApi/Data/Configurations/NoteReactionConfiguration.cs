using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class PostCommentReactionConfiguration : IEntityTypeConfiguration<NoteReaction>
    {
        public void Configure(EntityTypeBuilder<NoteReaction> builder)
        {
            builder.ToTable("note_reactions");

            builder.HasKey(noteReaction => new { noteReaction.NoteId, noteReaction.NerdId });
            builder.HasIndex(noteReaction => new { noteReaction.NoteId, noteReaction.Reaction });

            builder.Property(noteReaction => noteReaction.ReactedAt).HasDefaultValueSql("NOW()");
            builder.Property(noteReaction => noteReaction.Reaction).HasDefaultValue(Reaction.Kudos);

            builder.Property(noteReaction => noteReaction.NoteId).HasColumnName("post_comment_id");
            builder.Property(noteReaction => noteReaction.NerdId).HasColumnName("nerd_id");
            builder.Property(noteReaction => noteReaction.ReactedAt).HasColumnName("reacted_at");
            builder.Property(noteReaction => noteReaction.Reaction).HasColumnName("reaction");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:57 PM
# Update Fri, Jan  9, 2026  9:25:36 PM
# Update Fri, Jan  9, 2026  9:34:21 PM
// Logic update: dZ7DYLXizyTW
// Logic update: WIeLJ69kQLPV
// Logic update: gryyrlBsUw3b
// Logic update: H6iRh0QUuyWr
// Logic update: wPzyJLFkphmg
// Logic update: ZtPbVQHDTKzj
