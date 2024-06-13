using DailyStatistics.Application.Services.Errors.UserService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.DTO.Auth;
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

	[HttpPost("login")]
	public async Task<ActionResult<UserTokensPair?>> Login([FromBody] UserLoginData loginData)
	{
		var result = await _userService.LoginAsync(loginData);

		if (result)
		{
			return Ok(result.Value);
		}

		string message = result.Error switch
		{
			LoginErrors.InvalidCredentials => "Invalid credentials",
			_ => "An error occurred",
		};

		return BadRequest(message);
	}

	[HttpPost("refresh")]
	public async Task<ActionResult<LoginTokens?>> Refresh([FromBody] TokensPair pair)
	{
		var result = await _userService.RefreshAccessToken(pair.AccessToken, pair.RefreshToken);

		if (result)
		{
			return Ok(result.Value);
		}

		string message = result.Error switch
		{
			RefreshAccessTokenErrors.UserNotFound => "User not found",
			RefreshAccessTokenErrors.InvalidToken => "Invalid token",
			_ => "An error occurred",
		};

		return BadRequest(message);
	}

	[HttpPost("logout")]
	public async Task<ActionResult> Logout([FromHeader] string accessToken)
	{
		var result = await _userService.Logout(accessToken);

		if (!result)
		{
			string message = result.Error switch
			{
				LogoutErrors.InvalidToken => "Invalid token",
				_ => "An error occurred",
			};

			return BadRequest(message);
		}
		
		return Ok();
	}
}
