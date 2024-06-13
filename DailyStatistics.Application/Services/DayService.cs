using DailyStatistics.Application.Helpers;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.DayService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.DTO.Day;
using DailyStatistics.Model;
using DailyStatistics.Persistence.Repositories;

namespace DailyStatistics.Application.Services;

public class DayService : IDayService
{
	private readonly IDayRecordRepository _dayRecordRepository;

	public DayService(IDayRecordRepository dayRecordRepository)
	{
		_dayRecordRepository = dayRecordRepository;
	}

	public async Task<Result<DayDto, CreateDayError>> CreateDayAsync(DayCreate day, string userId)
	{
		DateOnly date = DayRecordHelper.MapDateToDateOnly(day.Date);
		if (await _dayRecordRepository.UserHasDayRecord(date, userId))
			return CreateDayError.DayAlreadyExists;

		DayRecord dayRecord = DayRecordHelper.MapDayDtoToDayRecord(day, userId);
		DayRecord? createdDayRecord = await _dayRecordRepository.AddDayAsync(dayRecord);

		if (createdDayRecord is null)
			return CreateDayError.DayNotCreated;

		DayDto dayDto = DayRecordHelper.MapDayRecordToDayDto(createdDayRecord);
		return dayDto;
	}

	public async Task<Result<DeleteDayError>> DeleteDayAsync(Guid id, string userId)
	{
		if (!await _dayRecordRepository.UserHasDayRecord(id, userId))
			return DeleteDayError.DayNotFound;

		bool deleted = await _dayRecordRepository.DeleteDayAsync(id);

		if (!deleted)
			return DeleteDayError.DayNotDeleted;

		return Result.Ok<DeleteDayError>();
	}

	public async Task<Result<IEnumerable<DateOnly>, GetDaysError>> GetAllDayRecordDates(string userId)
	{
		IEnumerable<DateOnly> dates = await _dayRecordRepository.GetAllDayRecordDates(userId);

		if (!dates.Any())
			return GetDaysError.NoDaysFound;

		return new Result<IEnumerable<DateOnly>, GetDaysError>() { Value = dates };
	}

	public async Task<Result<IEnumerable<DayDto>, GetDaysError>> GetDaysAsync(FirstAndLastDayPair range, string userId)
	{
		DateOnly from = DayRecordHelper.MapDateToDateOnly(range.FirstDay);
		DateOnly to = DayRecordHelper.MapDateToDateOnly(range.LastDay);
		IEnumerable<DayRecord> dayRecords = await _dayRecordRepository.GetDaysAsync(from, to, userId);

		if (!dayRecords.Any())
			return GetDaysError.NoDaysFound;

		return new Result<IEnumerable<DayDto>, GetDaysError>() { Value = dayRecords.Select(DayRecordHelper.MapDayRecordToDayDto) };
	}

	public async Task<Result<FirstAndLastDayPair?, GetDaysError>> GetFirstAndLastDayAsync(string userId)
	{
		(DateOnly, DateOnly)? firstAndLastDay = await _dayRecordRepository.GetFirstAndLastDayAsync(userId);

		if (firstAndLastDay is null)
			return GetDaysError.NoDaysFound;

		FirstAndLastDayPair pair = DayRecordHelper.MapTupleToPair(firstAndLastDay.Value);
		return pair;
	}

	public async Task<Result<DayDto, UpdateDayError>> UpdateDayAsync(DayDto dayDto, string userId)
	{
		DayRecord? dayRecord = await _dayRecordRepository.GetDayByIdAsync(dayDto.Id, userId);

		if (dayRecord is null)
			return UpdateDayError.DayNotFound;

		DateOnly date = DayRecordHelper.MapDateToDateOnly(dayDto.Date);
		if (dayRecord.Date != date && await _dayRecordRepository.UserHasDayRecord(date, userId))
			return UpdateDayError.RecordWithThisDateAlreadyExists;

		DayRecord dayToUpdate = DayRecordHelper.MapDayDtoToDayRecord(dayDto, userId);
		DayRecord? updatedDayRecord = await _dayRecordRepository.UpdateDayAsync(dayToUpdate);

		if (updatedDayRecord is null)
			return UpdateDayError.DayNotUpdated;

		DayDto updatedDayDto = DayRecordHelper.MapDayRecordToDayDto(updatedDayRecord);
		return updatedDayDto;
	}
}
