using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;
using Persic.EF.Postgres.Search;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class NerdConfiguration : IEntityTypeConfiguration<Nerd>
    {
        public void Configure(EntityTypeBuilder<Nerd> builder)
        {
            builder.ToTable("nerds");

            builder.HasIndex(nerd => nerd.Email).IsUnique();
            builder.HasIndex(nerd => nerd.UserName).IsUnique();
            builder.HasIndexedSearchVectorGeneratedFrom(nerd => nerd.UserName!);

            builder.Property(nerd => nerd.CreatedAt).HasDefaultValueSql("NOW()");
            builder.Property(nerd => nerd.NsfwOption).HasDefaultValue(NsfwOption.Blur);
        }
    }

}
# File update Fri, Jan  9, 2026  9:15:57 PM
# Update Fri, Jan  9, 2026  9:25:35 PM
# Update Fri, Jan  9, 2026  9:34:21 PM
// Logic update: ASrIG07IEaC7
// Logic update: utECjJLlGsDh
// Logic update: HAC3qkpwfjqR
// Logic update: YZKeUvK1lKCb
// Logic update: W33N1aeRx9bC
