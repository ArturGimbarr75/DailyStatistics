using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Services.Errors.ActivityKindService;
using DailyStatistics.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyStatistics.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ActivityKindController : RepairControllerBase
{
	private readonly IActivityKindService _activityKindService;

	public ActivityKindController(IActivityKindService activityKindService)
	{
		_activityKindService = activityKindService;
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("create")]
	public async Task<ActionResult<ActivityKindDto>> CreateActivityKind([FromBody] ActivityKindCreate request)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityKindService.CreateActivityKind(userId, request);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			CreateActivityKindErrors.InvalidName => "Invalid name",
			CreateActivityKindErrors.InvalidUserId => "Invalid user ID",
			CreateActivityKindErrors.UserAlreadyHasActivityKindWithThisName => "User already has activity kind with this name",
			_ => "An error occurred",
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpDelete("delete")]
	public async Task<IActionResult> DeleteActivityKind([FromQuery] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityKindService.DeleteActivityKind(userId, id);

		if (result)
			return Ok();

		string message = result.Error switch
		{
			DeleteActivityKindErrors.UserDoesNotHaveActivityKindWithThisId => "User does not have activity kind with this ID",
			DeleteActivityKindErrors.InvalidId => "Invalid ID",
			_ => "An error occurred",
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get")]
	public async Task<ActionResult<ActivityKindDto>> GetActivityKind([FromBody] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityKindService.GetActivityKind(userId, id);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			GetActivityKindErrors.ActivityKindNotFound => "Activity kind not found",
			GetActivityKindErrors.UserDoesNotHaveActivityKindWithThisId => "User does not have activity kind with this ID",
			_ => "An error occurred",
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-all")]
	public async Task<ActionResult<IEnumerable<ActivityKindDto>>> GetAllActivityKinds()
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityKindService.GetAllActivityKinds(userId);

		if (result)
			return Ok(result.Value);

		if (result.Error == GetActivityKindErrors.ActivityKindNotFound)
			return NotFound("Activity kinds not found");

		string message = result.Error switch
		{
			_ => "An error occurred",
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPut("update")]
	public async Task<ActionResult<ActivityKindDto>> UpdateActivityKind([FromBody] ActivityKindDto request)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityKindService.UpdateActivityKind(userId, request);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			UpdateActivityKindErrors.ActivityKindNotFound => "Activity kind not found",
			UpdateActivityKindErrors.UserDoesNotHaveActivityKindWithThisId => "User does not have activity kind with this ID",
			UpdateActivityKindErrors.InvalidName => "Invalid name",
			UpdateActivityKindErrors.UserAlreadyHasActivityKindWithThisName => "User already has activity kind with this name",
			_ => "An error occurred",
		};

		return BadRequest(message);
	}
}
