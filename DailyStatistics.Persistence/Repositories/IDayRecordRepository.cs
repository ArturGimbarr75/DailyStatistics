using DailyStatistics.Model;

namespace DailyStatistics.Persistence.Repositories;

public interface IDayRecordRepository
{
	Task<bool> UserHasDayRecord(DateOnly date, string userId);
	Task<bool> UserHasDayRecord(Guid id, string userId);
	Task<DayRecord?> GetDayByIdAsync(Guid id, string userId);
	Task<DayRecord?> GetDayAsync(DateOnly date, string userId);
	Task<IEnumerable<DayRecord>> GetDaysAsync(DateOnly from, DateOnly to, string userId);
	Task<(DateOnly, DateOnly)?> GetFirstAndLastDayAsync(string userId);
	Task<IEnumerable<DateOnly>> GetAllDayRecordDates(string userId);
	Task<DayRecord?> AddDayAsync(DayRecord dayRecord);
	Task<DayRecord?> UpdateDayAsync(DayRecord dayRecord);
	Task<bool> DeleteDayAsync(Guid id);
}
