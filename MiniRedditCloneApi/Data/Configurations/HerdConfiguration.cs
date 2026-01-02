using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;
using Persic.EF.Postgres.Search;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class HerdConfiguration : IEntityTypeConfiguration<Herd>
    {
        public void Configure(EntityTypeBuilder<Herd> builder)
        {
            builder.ToTable("herds");

            builder.HasIndex(herd => herd.Name).IsUnique();
            builder.HasIndexedSearchVectorGeneratedFrom(herd => herd.Name);
            builder.Property(herd => herd.Description).HasMaxLength(100);

            builder.Property(herd => herd.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(herd => herd.AllowNSFW).HasDefaultValue(false);

            builder.Property(herd => herd.Id).HasColumnName("id");
            builder.Property(herd => herd.CreatedAt).HasColumnName("created_at");
            builder.Property(herd => herd.Name).HasColumnName("name");
            builder.Property(herd => herd.Description).HasColumnName("description");
            builder.Property(herd => herd.Rules).HasColumnName("rules");
            builder.Property(herd => herd.Image).HasColumnName("image");
            builder.Property(herd => herd.AllowNSFW).HasColumnName("allow_nsfw");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:55 PM
# Update Fri, Jan  9, 2026  9:25:33 PM
# Update Fri, Jan  9, 2026  9:34:19 PM
// Logic update: ZTbM1YZm3oKF
// Logic update: Mp5ploAtWJbK
// Logic update: VpsKKkofYuKK
// Logic update: 8eVTpNw8WrbU
// Logic update: gHvZMxmA6B1M
// Logic update: TWsH73VfTWhd
