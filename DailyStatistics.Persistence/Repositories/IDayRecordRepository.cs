﻿using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Persistence.Repositories;

public interface IDayRecordRepository
{
	Task<bool> UserHasDayRecord(DateOnly date, string userId);
	Task<DayRecord?> GetDayAsync(DateOnly date, string userId);
	Task<IEnumerable<DayRecord>> GetDaysAsync(DateOnly from, DateOnly to, string userId);
	Task<(DateOnly, DateOnly)?> GetFirstAndLastDayAsync(string userId);
	Task<IEnumerable<DateOnly>> GetAllDatesWithRecords(string userId);
	Task<DayRecord?> AddDayAsync(DayRecord dayRecord);
	Task<DayRecord?> UpdateDayAsync(DayRecord dayRecord);
	Task<bool> DeleteDayAsync(Guid id);
}
