using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class DomainConfiguration : IEntityTypeConfiguration<Domain>
    {
        public void Configure(EntityTypeBuilder<Domain> builder)
        {
            builder.ToTable("domains");

            builder.HasIndex(domain => domain.Name).IsUnique();

            builder.Property(domain => domain.Id).HasColumnName("id");
            builder.Property(domain => domain.Name).HasColumnName("name");
            builder.Property(domain => domain.IsSystem).HasColumnName("is_system");

            builder.HasData(
                new Domain { Id = 1, Name = "Science" },
                new Domain { Id = 2, Name = "Technology & Computing" },
                new Domain { Id = 3, Name = "Mathematics" },
                new Domain { Id = 4, Name = "Arts & Literature" },
                new Domain { Id = 5, Name = "Culture & Humanities" },
                new Domain { Id = 6, Name = "Gaming & Entertainment" },
                new Domain { Id = 7, Name = "Space & Astronomy" },
                new Domain { Id = 8, Name = "Engineering & Making" }
            );
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:54 PM
# Update Fri, Jan  9, 2026  9:25:33 PM
# Update Fri, Jan  9, 2026  9:34:18 PM
// Logic update: MPDm03USMCib
// Logic update: 7E0vHXEHnF8O
// Logic update: Veyx4QVIruQe
// Logic update: zMvIzMbhyBOa
// Logic update: QjGjBB5CokXx
