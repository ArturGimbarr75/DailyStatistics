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
	private readonly ITokenService _tokensService;

	public ActivityKindService(ITrackingActivityKindRepository activityKindRepository, ITokenService tokensService)
	{
		_activityKindRepository = activityKindRepository;
		_tokensService = tokensService;
	}

	public async Task<Result<ActivityKindDto, CreateActivityKindErrors>> CreateActivityKind(string jwt, ActivityKindCreate activityKind)
	{
		string? userId = _tokensService.GetUserIdFromToken(jwt);

		if (userId is null)
			return CreateActivityKindErrors.InvalidUserId;

		if (string.IsNullOrWhiteSpace(activityKind.Name))
			return CreateActivityKindErrors.InvalidName;

		if (await _activityKindRepository.ExistsWithNamesAsync(userId, activityKind.Name))
			return CreateActivityKindErrors.UserAlreadyHasActivityKindWithThisName;

		TrackingActivityKind trackingActivityKind = ActivityKindHelper.MapActivityKindCreateToActivityKind(activityKind);
		TrackingActivityKind? createdActivityKind = await _activityKindRepository.AddAsync(trackingActivityKind);

		if (createdActivityKind is null)
			return CreateActivityKindErrors.InternalError;

		ActivityKindDto activityKindDto = ActivityKindHelper.MapActivityKindToDto(createdActivityKind);
		return activityKindDto;
	}

	public async Task<Result<DeleteActivityKindErrors>> DeleteActivityKind(string jwt, Guid id)
	{
		string? userId = _tokensService.GetUserIdFromToken(jwt);

		if (userId is null)
			return DeleteActivityKindErrors.InvalidJwt;

		if (!await _activityKindRepository.UserOwnsTrackingActivityKind(userId, id))
			return DeleteActivityKindErrors.UserDoesNotHaveActivityKindWithThisId;

		bool deleted = await _activityKindRepository.DeleteAsync(id);

		if (!deleted)
			return DeleteActivityKindErrors.InternalError;

		return Result.Ok<DeleteActivityKindErrors>();
	}

	public async Task<Result<ActivityKindDto, GetActivityKindErrors>> GetActivityKind(string jwt, Guid id)
	{
		string? userId = _tokensService.GetUserIdFromToken(jwt);

		if (userId is null)
			return GetActivityKindErrors.InvalidJwt;

		TrackingActivityKind? activityKind = await _activityKindRepository.GetAsync(id);

		if (activityKind is null)
			return GetActivityKindErrors.ActivityKindNotFound;

		if (activityKind.UserId != userId)
			return GetActivityKindErrors.UserDoesNotHaveActivityKindWithThisId;

		ActivityKindDto activityKindDto = ActivityKindHelper.MapActivityKindToDto(activityKind);

		return activityKindDto;
	}

	public async Task<Result<IEnumerable<ActivityKindDto>, GetActivityKindErrors>> GetAllActivityKinds(string jwt)
	{
		string? userId = _tokensService.GetUserIdFromToken(jwt);

		if (userId is null)
			return GetActivityKindErrors.InvalidJwt;

		IEnumerable<TrackingActivityKind> activityKinds = await _activityKindRepository.GetAllOfUserAsync(userId);

		if (!activityKinds.Any())
			return GetActivityKindErrors.ActivityKindNotFound;

		IEnumerable<ActivityKindDto> activityKindDtos = activityKinds.Select(ActivityKindHelper.MapActivityKindToDto);
		return new() { Value = activityKindDtos };
	}

	public async Task<Result<ActivityKindDto, UpdateActivityKindErrors>> UpdateActivityKind(string jwt, ActivityKindDto activityKind)
	{
		string? userId = _tokensService.GetUserIdFromToken(jwt);

		if (userId is null)
			return UpdateActivityKindErrors.InvalidJwt;

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
