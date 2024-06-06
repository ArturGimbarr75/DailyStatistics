namespace DailyStatistics.Application.DTO;

public sealed class ActivityKindDto
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string UserId { get; set; } = string.Empty;
	public string? Description { get; set; }
}
