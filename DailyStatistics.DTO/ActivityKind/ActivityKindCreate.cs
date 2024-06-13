namespace DailyStatistics.DTO.ActivityKind;

public sealed class ActivityKindCreate
{
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
}
