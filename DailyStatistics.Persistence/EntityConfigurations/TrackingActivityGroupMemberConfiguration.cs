using DailyStatistics.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyStatistics.Persistence.EntityConfigurations;

internal class TrackingActivityGroupMemberConfiguration : IEntityTypeConfiguration<TrackingActivityGroupMember>
{
	public void Configure(EntityTypeBuilder<TrackingActivityGroupMember> builder)
	{
		builder.HasOne(t => t.TrackingActivityKind)
			.WithMany(t => t.TrackingActivityGroupMembers)
			.HasForeignKey(t => t.TrackingActivityKindId)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(t => t.TrackingActivityGroup)
			.WithMany(t => t.TrackingActivityGroupMembers)
			.HasForeignKey(t => t.TrackingActivityGroupId)
			.OnDelete(DeleteBehavior.NoAction);
	}
}
