namespace DailyStatistics.Model;

public abstract class CreationTrackingEntity
{
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
