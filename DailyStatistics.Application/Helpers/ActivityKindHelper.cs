using DailyStatistics.Application.DTO;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Helpers;

internal static class ActivityKindHelper
{
	public static TrackingActivityKind MapActivityKindCreateToActivityKind(ActivityKindCreate activityKindCreate)
	{
		return new()
		{
			Name = activityKindCreate.Name,
			UserId = activityKindCreate.UserId,
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
}
