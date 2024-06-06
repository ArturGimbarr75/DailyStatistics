using DailyStatistics.Application.DTO;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Helpers;

internal static class ActivityKindHelper
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
