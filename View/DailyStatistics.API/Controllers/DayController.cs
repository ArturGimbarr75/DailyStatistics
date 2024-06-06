using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Services.Errors.DayService;
using DailyStatistics.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyStatistics.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class DayController : RepairControllerBase
{
	private readonly IDayService _dayService;

	public DayController(IDayService dayService)
	{
		_dayService = dayService;
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("create")]
	public async Task<ActionResult<DayDto>> CreateDay([FromBody] DayCreate request)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _dayService.CreateDayAsync(request, userId);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			CreateDayError.DayNotCreated => "Day not created",
			CreateDayError.DayAlreadyExists => "Day already exists",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("delete")]
	public async Task<IActionResult> DeleteDay([FromBody] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _dayService.DeleteDayAsync(id, userId);

		if (result)
			return Ok();

		string message = result.Error switch
		{
			DeleteDayError.DayNotFound => "Day not found",
			DeleteDayError.DayNotDeleted => "Day not deleted",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("update")]
	public async Task<ActionResult<DayDto>> UpdateDay([FromBody] DayDto request)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _dayService.UpdateDayAsync(request, userId);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			UpdateDayError.DayNotFound => "Day not found",
			UpdateDayError.DayNotUpdated => "Day not updated",
			UpdateDayError.RecordWithThisDateAlreadyExists => "Record with this date already exists",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-all")]
	public async Task<ActionResult<IEnumerable<DayDto>>> GetAllDays()
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _dayService.GetAllDayRecordDates(userId);

		if (result)
			return Ok(result.Value);

		if (result.Error == GetDaysError.NoDaysFound)
			return NotFound("Days not found");

		string message = result.Error switch
		{
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-ir-range")]
	public async Task<ActionResult<FirstAndLastDayPair>> GetDaysInRange([FromBody] FirstAndLastDayPair pair)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _dayService.GetDaysAsync(pair.FirstDay, pair.LastDay, userId);

		if (result)
			return Ok(result.Value);

		if (result.Error == GetDaysError.NoDaysFound)
			return NotFound("Days not found");

		string message = result.Error switch
		{
			_ => "An error occurred"
		};

		return BadRequest(message);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-records-range")]
	public async Task<ActionResult<FirstAndLastDayPair>> GetFirstAndLastDay()
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _dayService.GetFirstAndLastDayAsync(userId);

		if (result)
			return Ok(result.Value);

		string message = result.Error switch
		{
			GetDaysError.NoDaysFound => "Days not found",
			_ => "An error occurred"
		};

		return BadRequest(message);
	}
}
