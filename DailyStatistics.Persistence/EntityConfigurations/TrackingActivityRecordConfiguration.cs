using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyStatistics.Persistence.EntityConfigurations;

internal class TrackingActivityRecordConfiguration : IEntityTypeConfiguration<TrackingActivityRecord>
{
	public void Configure(EntityTypeBuilder<TrackingActivityRecord> builder)
	{
		builder.HasOne(t => t.TrackingActivityKind)
			.WithMany(t => t.TrackingActivityRecords)
			.HasForeignKey(t => t.TrackingActivityKindId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(t => t.DayRecord)
			.WithMany(t => t.TrackingActivityRecords)
			.HasForeignKey(t => t.DayRecordId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
