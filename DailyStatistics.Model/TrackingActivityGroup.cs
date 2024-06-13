namespace DailyStatistics.Model;

public sealed class TrackingActivityGroup : CreationTrackingEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; } = default!;
	public string UserId { get; set; } = default!;
	public User User { get; set; } = default!;
	public ICollection<TrackingActivityGroupMember> TrackingActivityGroupMembers { get; set; } = new List<TrackingActivityGroupMember>();
}
