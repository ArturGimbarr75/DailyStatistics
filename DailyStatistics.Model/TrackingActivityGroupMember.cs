namespace DailyStatistics.Model;

public sealed class TrackingActivityGroupMember : CreationTrackingEntity
{
	public Guid Id { get; set; }
	public Guid TrackingActivityGroupId { get; set; }
	public TrackingActivityGroup TrackingActivityGroup { get; set; } = default!;
	public Guid TrackingActivityKindId { get; set; }
	public TrackingActivityKind TrackingActivityKind { get; set; } = default!;
	public double Coefficient { get; set; }
}
