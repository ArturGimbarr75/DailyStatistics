using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Services.Interfaces;
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
}
