using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ActivityRecordService;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IActivityRecordService
{
	Task<Result<ActivityRecordDto, AddRecordErrors>> AddActivityRecordAsync(ActivityRecordDto activityRecordDto, string userId);
	Task<Result<ActivityRecordDto, UpdateRecordErrors>> UpdateActivityRecordAsync(ActivityRecordDto activityRecordDto, string userId);
	Task<Result<DeleteRecordErrors>> DeleteActivityRecordAsync(Guid id, string userId);
	Task<Result<ActivityRecordDto, GetRecordErrors>> GetActivityRecordAsync(Guid id, string userId);
	Task<Result<IEnumerable<ActivityRecordDto>, GetRecordErrors>> GetActivityRecordsFromDayAsync(DateOnly date, string userId);
}
