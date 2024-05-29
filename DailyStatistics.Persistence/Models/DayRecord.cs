namespace DailyStatistics.Persistence.Models;

public sealed class DayRecord : CreationTrackingEntity
{
	public Guid Id { get; set; }
	public string? Description { get; set; }
	public DateOnly Date { get; set; }
	public string UserId { get; set; } = default!;
	public User User { get; set; } = default!;
	public ICollection<TrackingActivityRecord> TrackingActivityRecords { get; set; } = new List<TrackingActivityRecord>();
}
