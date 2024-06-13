namespace DailyStatistics.Application.Services.Errors.ActivityRecordService;

public enum AddRecordErrors
{
	InvalidDayRecordId,
	DayRecordNotFound,
	DayAlreadyHasRecordWithThisActivityKind,
	ActivityKindNotFound,
	AmountIsNegative,
	RecordNotAdded
}
