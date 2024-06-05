namespace DailyStatistics.Application.DTO;

public sealed class ActivityKindCreate
{
	public string Name { get; set; } = string.Empty;
	public string UserId { get; set; } = string.Empty;
	public string? Description { get; set; }
}
