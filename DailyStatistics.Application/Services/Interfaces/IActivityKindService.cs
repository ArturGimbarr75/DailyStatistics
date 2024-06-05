using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ActivityKindService;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IActivityKindService
{
	Task<Result<ActivityKindDto, CreateActivityKindErrors>> CreateActivityKind(string jwt, ActivityKindCreate activityKind);
	Task<Result<ActivityKindDto, UpdateActivityKindErrors>> UpdateActivityKind(string jwt, ActivityKindDto activityKind);
	Task<Result<DeleteActivityKindErrors>> DeleteActivityKind(string jwt, Guid id);
	Task<Result<ActivityKindDto, GetActivityKindErrors>> GetActivityKind(string jwt, Guid id);
	Task<Result<IEnumerable<ActivityKindDto>, GetActivityKindErrors>> GetAllActivityKinds(string jwt);
}
