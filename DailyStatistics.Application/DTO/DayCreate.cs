namespace DailyStatistics.Application.DTO;

public sealed class DayCreate
{
	public DateOnly Date { get; set; }
	public string? Description { get; set; }
}
