namespace DailyStatistics.Application.Services.Errors.ActivityRecordService;

public enum UpdateRecordErrors
{
	RecordNotFound,
	RecordNotUpdated,
	InvalidDayRecordId,
	DayRecordNotFound,
	DayAlreadyHasRecordWithThisActivityKind,
	ActivityKindNotFound,
	AmountIsNegative
}
