using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ActivityKindService;
using DailyStatistics.DTO.ActivityKind;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IActivityKindService
{
	Task<Result<ActivityKindDto, CreateActivityKindErrors>> CreateActivityKind(string userId, ActivityKindCreate activityKind);
	Task<Result<ActivityKindDto, UpdateActivityKindErrors>> UpdateActivityKind(string juserIdwt, ActivityKindDto activityKind);
	Task<Result<DeleteActivityKindErrors>> DeleteActivityKind(string userId, Guid id);
	Task<Result<ActivityKindDto, GetActivityKindErrors>> GetActivityKind(string userId, Guid id);
	Task<Result<IEnumerable<ActivityKindDto>, GetActivityKindErrors>> GetAllActivityKinds(string userId);
}
