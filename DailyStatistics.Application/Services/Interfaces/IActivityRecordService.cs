using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ActivityRecordService;
using DailyStatistics.DTO.Record;
using DailyStatistics.DTO.Day;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IActivityRecordService
{
	Task<Result<ActivityRecordDto, AddRecordErrors>> AddActivityRecordAsync(ActivityRecordCreate activityRecordDto, string userId);
	Task<Result<ActivityRecordDto, UpdateRecordErrors>> UpdateActivityRecordAsync(ActivityRecordDto activityRecordDto, string userId);
	Task<Result<DeleteRecordErrors>> DeleteActivityRecordAsync(Guid id, string userId);
	Task<Result<ActivityRecordDto, GetRecordErrors>> GetActivityRecordAsync(Guid id, string userId);
	Task<Result<IEnumerable<ActivityRecordDto>, GetRecordErrors>> GetActivityRecordsFromDayAsync(Date date, string userId);
}
