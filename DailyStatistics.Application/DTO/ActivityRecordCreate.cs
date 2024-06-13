namespace DailyStatistics.Application.DTO;

public sealed class ActivityRecordCreate
{
	public Guid DayRecordId { get; set; }
	public Guid ActivityKindId { get; set; }
	public double Amount { get; set; }
}