namespace DailyStatistics.Application.Services.Errors.ActivityKindService;

public enum UpdateActivityKindErrors
{
	InvalidId,
	InvalidName,
	InvalidUserId,
	UserDoesNotHaveActivityKindWithThisId,
	UserAlreadyHasActivityKindWithThisName
}
