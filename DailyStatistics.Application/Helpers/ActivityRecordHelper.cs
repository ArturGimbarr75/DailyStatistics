using DailyStatistics.DTO.Record;
using DailyStatistics.Model;

namespace DailyStatistics.Application.Helpers;

public static class ActivityRecordHelper
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
