using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class HerdDomainConfiguration : IEntityTypeConfiguration<HerdDomain>
    {
        public void Configure(EntityTypeBuilder<HerdDomain> builder)
        {
            builder.ToTable("herd_domains");

            builder.HasKey(herdDomain => new { herdDomain.HerdId, herdDomain.DomainId });
            builder.HasIndex(herdDomain => herdDomain.DomainId);

            builder.Property(herdDomain => herdDomain.DomainId).HasColumnName("domain_id");
            builder.Property(herdDomain => herdDomain.HerdId).HasColumnName("herd_id");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:55 PM
# Update Fri, Jan  9, 2026  9:25:34 PM
# Update Fri, Jan  9, 2026  9:34:19 PM
// Logic update: GeWSFYj7LdqT
// Logic update: azq8BYWFipkO
// Logic update: dXZv3gjLXp8O
// Logic update: qNle6Fp9ErUd
// Logic update: XLYHi0lY9NG4
// Logic update: sx9p6L1zUYQ0
