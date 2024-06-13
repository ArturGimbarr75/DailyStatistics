namespace DailyStatistics.DTO.Day;

public sealed class DayDto
{
	public Guid Id { get; set; }
	public Date Date { get; set; }
	public string? Description { get; set; }
}
