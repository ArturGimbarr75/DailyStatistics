namespace DailyStatistics.Persistence.Models;

public abstract class CreationTrackingEntity
{
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
