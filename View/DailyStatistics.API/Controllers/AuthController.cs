using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Services.Errors.UserService;
using DailyStatistics.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyStatistics.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
	private readonly IUserService _userService;

	public AuthController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost("register")]
	public async Task<ActionResult<UserDto>> Register([FromBody] UserRegistrationData registrationData)
	{
		var result = await _userService.CreateAsync(registrationData);

		if (result)
			return Ok(result.Value);

		string[] message = result.Error switch
		{
			RegistrationErrors.PasswordsDoNotMatch => ["Passwords do not match"],
			_ => result.Info ?? ["An error occurred"],
		};

		return BadRequest(message);
	}
}
