using DailyStatistics.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyStatistics.Persistence.EntityConfigurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
	public void Configure(EntityTypeBuilder<RefreshToken> builder)
	{
		builder.HasOne(t => t.User)
			.WithMany(t => t.RefreshTokens)
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasIndex(t => t.Token);
	}
}
