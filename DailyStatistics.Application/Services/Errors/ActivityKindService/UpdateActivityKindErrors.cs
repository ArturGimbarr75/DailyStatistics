namespace DailyStatistics.Application.Services.Errors.ActivityKindService;

public enum UpdateActivityKindErrors
{
	InvalidJwt,
	InvalidName,
	UserDoesNotHaveActivityKindWithThisId,
	UserAlreadyHasActivityKindWithThisName,
	ActivityKindNotFound
}
