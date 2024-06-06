using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Helpers;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ActivityKindService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Persistence.Models;
using DailyStatistics.Persistence.Repositories;

namespace DailyStatistics.Application.Services;

public class ActivityKindService : IActivityKindService
{
	private readonly ITrackingActivityKindRepository _activityKindRepository;

	public ActivityKindService(ITrackingActivityKindRepository activityKindRepository)
	{
		_activityKindRepository = activityKindRepository;
	}

	public async Task<Result<ActivityKindDto, CreateActivityKindErrors>> CreateActivityKind(string userId, ActivityKindCreate activityKind)
	{
		if (string.IsNullOrWhiteSpace(activityKind.Name))
			return CreateActivityKindErrors.InvalidName;

		if (await _activityKindRepository.ExistsWithNamesAsync(userId, activityKind.Name))
			return CreateActivityKindErrors.UserAlreadyHasActivityKindWithThisName;

		TrackingActivityKind trackingActivityKind = ActivityKindHelper.MapActivityKindCreateToActivityKind(activityKind, userId);
		TrackingActivityKind? createdActivityKind = await _activityKindRepository.AddAsync(trackingActivityKind);

		if (createdActivityKind is null)
			return CreateActivityKindErrors.InternalError;

		ActivityKindDto activityKindDto = ActivityKindHelper.MapActivityKindToDto(createdActivityKind);
		return activityKindDto;
	}

	public async Task<Result<DeleteActivityKindErrors>> DeleteActivityKind(string userId, Guid id)
	{
		if (!await _activityKindRepository.UserOwnsTrackingActivityKind(userId, id))
			return DeleteActivityKindErrors.UserDoesNotHaveActivityKindWithThisId;

		bool deleted = await _activityKindRepository.DeleteAsync(id);

		if (!deleted)
			return DeleteActivityKindErrors.InternalError;

		return Result.Ok<DeleteActivityKindErrors>();
	}

	public async Task<Result<ActivityKindDto, GetActivityKindErrors>> GetActivityKind(string userId, Guid id)
	{
		TrackingActivityKind? activityKind = await _activityKindRepository.GetAsync(id);

		if (activityKind is null)
			return GetActivityKindErrors.ActivityKindNotFound;

		if (activityKind.UserId != userId)
			return GetActivityKindErrors.UserDoesNotHaveActivityKindWithThisId;

		ActivityKindDto activityKindDto = ActivityKindHelper.MapActivityKindToDto(activityKind);

		return activityKindDto;
	}

	public async Task<Result<IEnumerable<ActivityKindDto>, GetActivityKindErrors>> GetAllActivityKinds(string userId)
	{
		IEnumerable<TrackingActivityKind> activityKinds = await _activityKindRepository.GetAllOfUserAsync(userId);

		if (!activityKinds.Any())
			return GetActivityKindErrors.ActivityKindNotFound;

		IEnumerable<ActivityKindDto> activityKindDtos = activityKinds.Select(ActivityKindHelper.MapActivityKindToDto);
		return new() { Value = activityKindDtos };
	}

	public async Task<Result<ActivityKindDto, UpdateActivityKindErrors>> UpdateActivityKind(string userId, ActivityKindDto activityKind)
	{
		if (await _activityKindRepository.UserOwnsTrackingActivityKind(userId, activityKind.Id))
			return UpdateActivityKindErrors.UserDoesNotHaveActivityKindWithThisId;

		if (string.IsNullOrWhiteSpace(activityKind.Name))
			return UpdateActivityKindErrors.InvalidName;

		if (await _activityKindRepository.ExistsWithNamesAsync(userId, activityKind.Name))
			return UpdateActivityKindErrors.UserAlreadyHasActivityKindWithThisName;

		TrackingActivityKind trackingActivityKind = ActivityKindHelper.MapActivityKindDtoToActivityKind(activityKind);
		TrackingActivityKind? updatedActivityKind = await _activityKindRepository.UpdateAsync(trackingActivityKind);

		if (updatedActivityKind is null)
			return UpdateActivityKindErrors.ActivityKindNotFound;

		ActivityKindDto activityKindDto = ActivityKindHelper.MapActivityKindToDto(updatedActivityKind);
		return activityKindDto;
	}
}
