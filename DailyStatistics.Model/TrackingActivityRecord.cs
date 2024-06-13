namespace DailyStatistics.Model;

public sealed class TrackingActivityRecord : CreationTrackingEntity
{
	public Guid Id { get; set; }
	public Guid DayRecordId { get; set; }
	public DayRecord DayRecord { get; set; } = default!;
	public Guid TrackingActivityKindId { get; set; }
	public TrackingActivityKind TrackingActivityKind { get; set; } = default!;
	public double Amount { get; set; }
}
