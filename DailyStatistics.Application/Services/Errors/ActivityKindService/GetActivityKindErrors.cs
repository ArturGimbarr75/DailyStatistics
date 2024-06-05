namespace DailyStatistics.Application.Services.Errors.ActivityKindService;

public enum GetActivityKindErrors
{
	InvalidJwt,
	ActivityKindNotFound,
	UserDoesNotHaveActivityKindWithThisId
}
