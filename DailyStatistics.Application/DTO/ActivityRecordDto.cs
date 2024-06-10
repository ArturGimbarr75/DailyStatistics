namespace DailyStatistics.Application.DTO;

public class ActivityRecordDto
{
	public Guid Id { get; set; }
	public Guid DayRecordId { get; set; }
	public Guid ActivityKindId { get; set; }
	public double Amount { get; set; }
}
