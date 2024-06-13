using DailyStatistics.DTO.ActivityKind;
using DailyStatistics.Model;

namespace DailyStatistics.Application.Helpers;

public static class ActivityKindHelper
{
	public static TrackingActivityKind MapActivityKindCreateToActivityKind(ActivityKindCreate activityKindCreate, string userId)
	{
		return new()
		{
			Name = activityKindCreate.Name,
			UserId = userId,
			Description = activityKindCreate.Description
		};
	}

	public static ActivityKindDto MapActivityKindToDto(TrackingActivityKind activityKind)
	{
		return new ActivityKindDto
		{
			Id = activityKind.Id,
			Name = activityKind.Name,
			UserId = activityKind.UserId,
			Description = activityKind.Description
		};
	}

	public static TrackingActivityKind MapActivityKindDtoToActivityKind(ActivityKindDto activityKindDto)
	{
		return new()
		{
			Id = activityKindDto.Id,
			Name = activityKindDto.Name,
			UserId = activityKindDto.UserId,
			Description = activityKindDto.Description
		};
	}
}
