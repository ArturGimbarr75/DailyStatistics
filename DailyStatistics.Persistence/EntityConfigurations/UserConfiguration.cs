using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyStatistics.Persistence.EntityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasMany(t => t.DayRecords)
			.WithOne(t => t.User)
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(t => t.RefreshTokens)
			.WithOne(t => t.User)
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(t => t.TrackingActivityGroups)
			.WithOne(t => t.User)
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(t => t.TrackingActivityKinds)
			.WithOne(t => t.User)
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(t => t.ProfileImage)
			.WithOne(t => t.User)
			.HasForeignKey<ProfileImage>(t => t.UserId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
