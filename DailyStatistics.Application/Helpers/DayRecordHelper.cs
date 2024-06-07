using DailyStatistics.Application.DTO;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Helpers;

internal static class DayRecordHelper
{
	public static DayRecord MapDayDtoToDayRecord(DayDto dayDto, string userId)
	{
		return new()
		{
			Id = dayDto.Id,
			Date = dayDto.Date,
			Description = dayDto.Description,
			UserId = userId
		};
	}

	public static DayDto MapDayRecordToDayDto(DayRecord dayRecord)
	{
		return new()
		{
			Id = dayRecord.Id,
			Date = dayRecord.Date,
			Description = dayRecord.Description
		};
	}

	public static DayRecord MapDayDtoToDayRecord(DayCreate dayCreate, string userId)
	{
		return new()
		{
			Date = dayCreate.Date,
			Description = dayCreate.Description,
			UserId = userId
		};
	}

	public static FirstAndLastDayPair MapTupleToPair((DateOnly first, DateOnly last) tuple)
	{
		return new()
		{
			FirstDay = tuple.first,
			LastDay = tuple.last
		};
	}
}
