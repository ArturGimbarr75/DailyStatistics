using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyStatistics.Persistence.EntityConfigurations;

internal class TrackingActivityGroupConfiguration : IEntityTypeConfiguration<TrackingActivityGroup>
{
	public void Configure(EntityTypeBuilder<TrackingActivityGroup> builder)
	{
		builder.HasMany(t => t.TrackingActivityGroupMembers)
			.WithOne(t => t.TrackingActivityGroup)
			.HasForeignKey(t => t.TrackingActivityGroupId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
