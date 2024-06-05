namespace DailyStatistics.Application.Services.Errors.ActivityKindService;

public enum UpdateActivityKindErrors
{
	InvalidJwt,
	InvalidName,
	InvalidUserId,
	UserDoesNotHaveActivityKindWithThisId,
	UserAlreadyHasActivityKindWithThisName,
	ActivityKindNotFound
}
