using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Helpers;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ActivityRecordService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Persistence.Models;
using DailyStatistics.Persistence.Repositories;

namespace DailyStatistics.Application.Services;

public class ActivityRecordService : IActivityRecordService
{
	private readonly ITrackingActivityRecordRepository _activityRecordRepository;
	private readonly IDayRecordRepository _dayRecordRepository;
	private readonly ITrackingActivityKindRepository _activityKindRepository;

	public ActivityRecordService(ITrackingActivityRecordRepository trackingActivityRecordRepository,
								 IDayRecordRepository dayRecordRepository,
								 ITrackingActivityKindRepository activityKindRepository)
	{
		_activityRecordRepository = trackingActivityRecordRepository;
		_dayRecordRepository = dayRecordRepository;
		_activityKindRepository = activityKindRepository;
	}

	public async Task<Result<ActivityRecordDto, AddRecordErrors>> AddActivityRecordAsync(ActivityRecordDto activityRecordDto, string userId)
	{
		if (activityRecordDto.Amount < 0)
			return AddRecordErrors.AmountIsNegative;

		DayRecord? dayRecord = await _dayRecordRepository.GetDayByIdAsync(activityRecordDto.DayRecordId, userId);

		if (dayRecord is null)
			return AddRecordErrors.DayRecordNotFound;

		if (dayRecord.UserId != userId)
			return AddRecordErrors.InvalidDayRecordId;

		if (await _activityRecordRepository.DayHasRecordWithKind(dayRecord.Date, userId, activityRecordDto.ActivityKindId))
			return AddRecordErrors.DayAlreadyHasRecordWithThisActivityKind;

		if (!await _activityKindRepository.UserOwnsTrackingActivityKind(userId, activityRecordDto.ActivityKindId))
			return AddRecordErrors.ActivityKindNotFound;

		TrackingActivityRecord? activityRecord = ActivityRecordHelper.MapActivityRecordDtoToActivityRecord(activityRecordDto);
		TrackingActivityRecord? addedRecord = await _activityRecordRepository.AddTrackingActivityRecordAsync(activityRecord);

		if (addedRecord is null)
			return AddRecordErrors.RecordNotAdded;

		ActivityRecordDto addedRecordDto = ActivityRecordHelper.MapActivityRecordToActivityRecordDto(addedRecord);
		return addedRecordDto;
	}

	public async Task<Result<DeleteRecordErrors>> DeleteActivityRecordAsync(Guid id, string userId)
	{
		if (!await _activityRecordRepository.UserOwnsRecord(userId, id))
			return DeleteRecordErrors.InvalidActivityRecordId;

		bool isDeleted = await _activityRecordRepository.DeleteTrackingActivityRecordAsync(id);

		if (!isDeleted)
			return DeleteRecordErrors.RecordNotDeleted;

		return Result.Ok<DeleteRecordErrors>();
	}

	public async Task<Result<ActivityRecordDto, GetRecordErrors>> GetActivityRecordAsync(Guid id, string userId)
	{
		if (!await _activityRecordRepository.UserOwnsRecord(userId, id))
			return GetRecordErrors.RecordNotFound;

		TrackingActivityRecord? activityRecord = await _activityRecordRepository.GetTrackingActivityRecordAsync(id);

		if (activityRecord is null)
			return GetRecordErrors.RecordNotFound;

		ActivityRecordDto activityRecordDto = ActivityRecordHelper.MapActivityRecordToActivityRecordDto(activityRecord);
		return activityRecordDto;
	}

	public async Task<Result<IEnumerable<ActivityRecordDto>, GetRecordErrors>> GetActivityRecordsFromDayAsync(DateOnly date, string userId)
	{
		if (!await _dayRecordRepository.UserHasDayRecord(date, userId))
			return GetRecordErrors.DayRecordNotFound;

		IEnumerable<TrackingActivityRecord> activityRecords = await _activityRecordRepository.GetTrackingActivityRecordsFromDayAsync(date, userId);

		if (!activityRecords.Any())
			return GetRecordErrors.RecordNotFound;

		IEnumerable<ActivityRecordDto> activityRecordsDto = activityRecords.Select(ActivityRecordHelper.MapActivityRecordToActivityRecordDto);
		return new() { Value = activityRecordsDto };
	}

	public async Task<Result<ActivityRecordDto, UpdateRecordErrors>> UpdateActivityRecordAsync(ActivityRecordDto activityRecordDto, string userId)
	{
		if (activityRecordDto.Amount < 0)
			return UpdateRecordErrors.AmountIsNegative;

		DayRecord? dayRecord = await _dayRecordRepository.GetDayByIdAsync(activityRecordDto.DayRecordId, userId);

		if (dayRecord is null)
			return UpdateRecordErrors.DayRecordNotFound;

		if (!await _activityKindRepository.UserOwnsTrackingActivityKind(userId, activityRecordDto.ActivityKindId))
			return UpdateRecordErrors.ActivityKindNotFound;

		if (dayRecord.UserId != userId)
			return UpdateRecordErrors.InvalidDayRecordId;

		TrackingActivityRecord? activityRecord = await _activityRecordRepository.GetTrackingActivityRecordAsync(activityRecordDto.Id);

		if (activityRecord is null)
			return UpdateRecordErrors.RecordNotFound;

		if (activityRecord.DayRecordId != activityRecordDto.DayRecordId &&
			await _activityRecordRepository.DayHasRecordWithKind(dayRecord.Date, userId, activityRecordDto.ActivityKindId))
			return UpdateRecordErrors.DayAlreadyHasRecordWithThisActivityKind;

		TrackingActivityRecord recordToUpdate = ActivityRecordHelper.MapActivityRecordDtoToActivityRecord(activityRecordDto);
		TrackingActivityRecord? updatedRecord = await _activityRecordRepository.UpdateTrackingActivityRecordAsync(recordToUpdate);

		if (updatedRecord is null)
			return UpdateRecordErrors.RecordNotUpdated;

		ActivityRecordDto updatedDto = ActivityRecordHelper.MapActivityRecordToActivityRecordDto(updatedRecord);
		return updatedDto;
	}
}
