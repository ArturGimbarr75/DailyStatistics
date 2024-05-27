namespace DailyStatistics.Persistence.Models;

public sealed class TrackingActivityKind : CreationTrackingEntity
{
    public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string? Description { get; set; }
	public string UserId { get; set; } = default!;
	public User User { get; set; } = default!;
}
