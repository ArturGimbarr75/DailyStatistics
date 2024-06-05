namespace DailyStatistics.Application.Services.Errors.ActivityKindService;

public enum DeleteActivityKindErrors
{
	InvalidJwt,
	InvalidId,
	UserDoesNotHaveActivityKindWithThisId,
	InternalError,
	Success
}
