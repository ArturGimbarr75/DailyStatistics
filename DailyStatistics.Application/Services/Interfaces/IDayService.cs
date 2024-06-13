using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.DayService;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IDayService
{
	public Task<Result<DayDto, CreateDayError>> CreateDayAsync(DayCreate dayDto, string userId);
	public Task<Result<DeleteDayError>> DeleteDayAsync(Guid id, string userId);
	public Task<Result<DayDto, UpdateDayError>> UpdateDayAsync(DayDto dayDto, string userId);
	public Task<Result<IEnumerable<DayDto>, GetDaysError>> GetDaysAsync(FirstAndLastDayPair range, string userId);
	public Task<Result<FirstAndLastDayPair?, GetDaysError>> GetFirstAndLastDayAsync(string userId);
	public Task<Result<IEnumerable<DateOnly>, GetDaysError>> GetAllDayRecordDates(string userId);
}
