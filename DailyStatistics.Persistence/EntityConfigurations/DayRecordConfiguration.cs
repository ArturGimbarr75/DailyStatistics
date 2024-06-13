using DailyStatistics.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyStatistics.Persistence.EntityConfigurations;

internal class DayRecordConfiguration : IEntityTypeConfiguration<DayRecord>
{
	public void Configure(EntityTypeBuilder<DayRecord> builder)
	{
		builder.HasMany(t => t.TrackingActivityRecords)
			.WithOne(t => t.DayRecord)
			.HasForeignKey(t => t.DayRecordId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}
