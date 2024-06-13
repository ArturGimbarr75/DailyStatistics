using DailyStatistics.DTO.Day;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Helpers;

internal static class DayRecordHelper
{
	public static DayRecord MapDayDtoToDayRecord(DayDto dayDto, string userId)
	{
		return new()
		{
			Id = dayDto.Id,
			Date = MapDateToDateOnly(dayDto.Date),
			Description = dayDto.Description,
			UserId = userId
		};
	}

	public static DayDto MapDayRecordToDayDto(DayRecord dayRecord)
	{
		return new()
		{
			Id = dayRecord.Id,
			Date = MapDateOnlyToDate(dayRecord.Date),
			Description = dayRecord.Description
		};
	}

	public static DayRecord MapDayDtoToDayRecord(DayCreate dayCreate, string userId)
	{
		return new()
		{
			Date = MapDateToDateOnly(dayCreate.Date),
			Description = dayCreate.Description,
			UserId = userId
		};
	}

	public static FirstAndLastDayPair MapTupleToPair((DateOnly first, DateOnly last) tuple)
	{
		return new()
		{
			FirstDay = MapDateOnlyToDate(tuple.first),
			LastDay = MapDateOnlyToDate(tuple.last)
		};
	}

	public static DateOnly MapDateToDateOnly(Date date)
	{
		return new(date.Year, date.Month, date.Day);
	}

	public static Date MapDateOnlyToDate(DateOnly dateOnly)
	{
		return new()
		{
			Year = dateOnly.Year,
			Month = dateOnly.Month,
			Day = dateOnly.Day
		};
	}
}
