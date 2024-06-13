using DailyStatistics.Application.Services.Errors.ProfileImageService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.DTO.Image;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailyStatistics.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ImageController : RepairControllerBase
{
	private readonly IProfileImageService _profileImageService;

	public ImageController(IProfileImageService profileImageService)
	{
		_profileImageService = profileImageService;
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPost("upload")]
	public async Task<ActionResult<ImageDto>> UploadImage([FromForm] UploadImageDto file)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		if (file is null)
			return BadRequest("No file uploaded");

		var result = await _profileImageService.UploadImage(file, userId);

		if (result)
			return Ok(result.Value);

		return BadRequest(result.Info);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-file")]
	public async Task<IActionResult> GetProfileImage([FromQuery] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _profileImageService.GetImage(userId, id);

		if (result)
		{
			string path = result.Value!.Value.path;
			MemoryStream memoryStream = new();
			await using FileStream fileStream = new(path, FileMode.Open);
			await fileStream.CopyToAsync(memoryStream);
			memoryStream.Position = 0;
			return File(memoryStream, "image/*");
		}

		string error = result.Error switch
		{
			GetImageError.UserNotFound => "User not found",
			GetImageError.ImageNotFound => "Image not found",
			_ => "Unknown error"
		};

		return BadRequest(error);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpPut("set-profile-image")]
	public async Task<IActionResult> SetProfileImage([FromQuery] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _profileImageService.SetProfileImage(userId, id);

		if (result)
			return Ok();

		string error = result.Error switch
		{
			SetProfileImageError.UserNotFound => "User not found",
			SetProfileImageError.ImageNotFound => "Image not found",
			SetProfileImageError.ImageNotSet => "Image not set",
			_ => "Unknown error"
		};

		return BadRequest(error);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpDelete("delete-image")]
	public async Task<IActionResult> DeleteImage([FromQuery] Guid id)
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _profileImageService.DeleteImage(userId, id);

		if (result)
			return Ok();

		string error = result.Error switch
		{
			DeleteImageError.UserNotFound => "User not found",
			DeleteImageError.ImageNotFound => "Image not found",
			DeleteImageError.ImageNotDeleted => "Image not deleted",
			_ => "Unknown error"
		};

		return BadRequest(error);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-profile-image")]
	public async Task<ActionResult<ImageDto>> GetProfileImage()
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _profileImageService.GetProfileImage(userId);

		if (result)
			if (result.Value is null)
				return NoContent();
			else
				return Ok(result.Value);

		string error = result.Error switch
		{
			GetImageError.UserNotFound => "User not found",
			GetImageError.ImageNotFound => "Image not found",
			_ => "Unknown error"
		};

		return BadRequest(error);
	}

	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[HttpGet("get-images")]
	public async Task<ActionResult<IEnumerable<ImageDto>>> GetImages()
	{
		string? userId = UserId;

		if (userId is null)
			return BadRequest("Invalid JWT");

		var result = await _profileImageService.GetImages(userId);

		if (result)
			return Ok(result.Value);

		string error = result.Error switch
		{
			GetImageError.UserNotFound => "User not found",
			GetImageError.ImageNotFound => "Image not found",
			_ => "Unknown error"
		};

		return BadRequest(error);
	}
}
