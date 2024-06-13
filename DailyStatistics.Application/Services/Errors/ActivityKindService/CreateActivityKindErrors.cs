namespace DailyStatistics.Application.Services.Errors.ActivityKindService;

public enum CreateActivityKindErrors
{
	InvalidName,
	InvalidUserId,
	UserAlreadyHasActivityKindWithThisName,
	InternalError
}
