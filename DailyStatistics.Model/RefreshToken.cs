namespace DailyStatistics.Model;

public sealed class RefreshToken : CreationTrackingEntity
{
	public Guid Id { get; set; }
	public string Token { get; set; } = default!;
	public DateTime Expires { get; set; }
	public string UserId { get; set; } = default!;
	public User User { get; set; } = default!;
}
