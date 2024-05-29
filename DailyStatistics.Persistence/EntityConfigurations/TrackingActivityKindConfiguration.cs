using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyStatistics.Persistence.EntityConfigurations;

internal class TrackingActivityKindConfiguration : IEntityTypeConfiguration<TrackingActivityKind>
{
	public void Configure(EntityTypeBuilder<TrackingActivityKind> builder)
	{
		builder.HasMany(t => t.TrackingActivityGroupMembers)
			.WithOne(t => t.TrackingActivityKind)
			.HasForeignKey(t => t.TrackingActivityKindId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasMany(t => t.TrackingActivityRecords)
			.WithOne(t => t.TrackingActivityKind)
			.HasForeignKey(t => t.TrackingActivityKindId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
