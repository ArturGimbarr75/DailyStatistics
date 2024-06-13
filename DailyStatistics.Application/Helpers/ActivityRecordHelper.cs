using DailyStatistics.DTO.Record;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Helpers;

internal static class ActivityRecordHelper
{
    public static TrackingActivityRecord MapActivityRecordDtoToActivityRecord(ActivityRecordDto activityRecordDto)
	{
		return new()
		{
			Id = activityRecordDto.Id,
			Amount = activityRecordDto.Amount,
			DayRecordId = activityRecordDto.DayRecordId,
			TrackingActivityKindId = activityRecordDto.ActivityKindId,
		};
	}

	public static ActivityRecordDto MapActivityRecordToActivityRecordDto(TrackingActivityRecord activityRecord)
	{
		return new()
		{
			Id = activityRecord.Id,
			Amount = activityRecord.Amount,
			DayRecordId = activityRecord.DayRecordId,
			ActivityKindId = activityRecord.TrackingActivityKindId,
		};
	}

	public static TrackingActivityRecord MapActivityRecordCreateToActivityRecord(ActivityRecordCreate activityRecordDto)
	{
		return new()
		{
			Amount = activityRecordDto.Amount,
			DayRecordId = activityRecordDto.DayRecordId,
			TrackingActivityKindId = activityRecordDto.ActivityKindId,
		};
	}
}
