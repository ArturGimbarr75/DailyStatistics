namespace DailyStatistics.Persistence.Models;

public sealed class ProfileImage
{
	public Guid Id { get; set; }
	public string? UserId { get; set; } = default!;
	public string ImagePath { get; set; } = default!;
}
