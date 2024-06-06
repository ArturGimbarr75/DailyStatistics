namespace DailyStatistics.Application.Services.Errors.ActivityKindService;

public enum UpdateActivityKindErrors
{
	InvalidName,
	UserDoesNotHaveActivityKindWithThisId,
	UserAlreadyHasActivityKindWithThisName,
	ActivityKindNotFound
}
