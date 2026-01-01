using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniRedditCloneApi.Models;

namespace MiniRedditCloneApi.Data.Configurations
{
    public class BanConfiguration : IEntityTypeConfiguration<Ban>
    {
        public void Configure(EntityTypeBuilder<Ban> builder)
        {
            builder.ToTable("bans");

            builder.HasKey(ban => new { ban.NerdId, ban.HerdId });
            builder.HasIndex(ban => ban.HerdId);
            builder.Property(ban => ban.BannedAt).HasDefaultValueSql("NOW()");

            builder.Property(ban => ban.NerdId).HasColumnName("nerd_id");
            builder.Property(ban => ban.HerdId).HasColumnName("herd_id");
            builder.Property(ban => ban.Reason).HasColumnName("reason");
            builder.Property(ban => ban.BannedAt).HasColumnName("banned_at");
        }
    }
}
# File update Fri, Jan  9, 2026  9:15:54 PM
# Update Fri, Jan  9, 2026  9:25:32 PM
# Update Fri, Jan  9, 2026  9:34:18 PM
// Logic update: FayYT8PLFtQq
// Logic update: 7ziIbux23zUm
// Logic update: DthM8tybjEsK
// Logic update: l3Iulx1uMipF
// Logic update: ruuemOWTjQFH
// Logic update: MbsRU90ErDiw
