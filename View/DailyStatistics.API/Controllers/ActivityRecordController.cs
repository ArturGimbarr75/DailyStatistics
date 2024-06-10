using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Services.Errors.ActivityRecordService;
using DailyStatistics.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyStatistics.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ActivityRecordController : RepairControllerBase
{
	private readonly IActivityRecordService _activityRecordService;

	public ActivityRecordController(IActivityRecordService activityRecordService)
	{
		_activityRecordService = activityRecordService;
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("create")]
	public async Task<ActionResult<ActivityRecordDto>> CreateActivityRecord([FromBody] ActivityRecordDto request)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityRecordService.AddActivityRecordAsync(request, userId);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			AddRecordErrors.ActivityKindNotFound => "Activity kind not found",
			AddRecordErrors.InvalidDayRecordId => "Invalid day record id",
			AddRecordErrors.DayRecordNotFound => "Day record not found",
			AddRecordErrors.DayAlreadyHasRecordWithThisActivityKind => "Day already has record with this activity kind",
			AddRecordErrors.AmountIsNegative => "Amount is negative",
			AddRecordErrors.RecordNotAdded => "Record not added",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("update")]
	public async Task<ActionResult<ActivityRecordDto>> UpdateActivityRecord([FromBody] ActivityRecordDto request)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityRecordService.UpdateActivityRecordAsync(request, userId);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			UpdateRecordErrors.ActivityKindNotFound => "Activity kind not found",
			UpdateRecordErrors.InvalidDayRecordId => "Invalid day record id",
			UpdateRecordErrors.DayRecordNotFound => "Day record not found",
			UpdateRecordErrors.RecordNotFound => "Record not found",
			UpdateRecordErrors.RecordNotUpdated => "Record not updated",
			UpdateRecordErrors.DayAlreadyHasRecordWithThisActivityKind => "Day already has record with this activity kind",
			UpdateRecordErrors.AmountIsNegative => "Amount is negative",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("delete")]
	public async Task<IActionResult> DeleteActivityRecord([FromBody] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityRecordService.DeleteActivityRecordAsync(id, userId);

		if (result)
			return Ok();

		string message = result.Error switch
		{
			DeleteRecordErrors.RecordNotDeleted => "Record not deleted",
			DeleteRecordErrors.InvalidActivityRecordId => "Invalid activity record id",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get")]
	public async Task<ActionResult<ActivityRecordDto>> GetActivityRecord([FromQuery] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityRecordService.GetActivityRecordAsync(id, userId);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			GetRecordErrors.RecordNotFound => "Record not found",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-from-day")]
	public async Task<ActionResult<IEnumerable<ActivityRecordDto>>> GetActivityRecordsFromDay([FromQuery] DateOnly date)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _activityRecordService.GetActivityRecordsFromDayAsync(date, userId);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			GetRecordErrors.DayRecordNotFound => "Day not found",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}
}
