using DailyStatistics.Application.Helpers;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.UserService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.DTO.Auth;
using DailyStatistics.Model;
using DailyStatistics.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Application.Services;

public sealed class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IRefreshTokenRepository _refreshTokenRepository;
	private readonly ITokenService _tokenService;
	private readonly IUserManagerFacade _userManager;

	public UserService( IUserRepository userRepository,
						IUserManagerFacade userManager,
						ITokenService tokenService,
						IRefreshTokenRepository refreshTokenRepository)
	{
		_userRepository = userRepository;
		_userManager = userManager;
		_tokenService = tokenService;
		_refreshTokenRepository = refreshTokenRepository;
	}

	public async Task<InfoResult<UserDto?, RegistrationErrors>> CreateAsync(UserRegistrationData registrationData)
	{
		if (registrationData.Password != registrationData.ConfirmPassword)
			return RegistrationErrors.PasswordsDoNotMatch;

		User user = RegistationHelper.MapRegistrationData(registrationData);
		var (createdUser, identityResult) = await _userRepository.AddUserAsync(user, registrationData.Password);

		if (!identityResult.Succeeded)
			return InfoResult<UserDto?, RegistrationErrors>.WithInfo(RegistrationErrors.Other, identityResult.Errors.Select(e => e.Description).ToArray());

		return RegistationHelper.MapUserToDto(createdUser!);
	}

	public async Task<InfoResult<UserTokensPair?, LoginErrors>> LoginAsync(UserLoginData loginData)
	{
		User? user = await _userRepository.GetUserByEmailAsync(loginData.UserNameOrEmail);
		user ??= await _userRepository.GetUserByUserNameAsync(loginData.UserNameOrEmail);

		if (user is null)
			return LoginErrors.InvalidCredentials;

		var verificationResult = _userManager.VerifyHashedPassword(user, user.PasswordHash!, loginData.Password);
		if (verificationResult == PasswordVerificationResult.Failed)
			return LoginErrors.InvalidCredentials;

		if (verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
		{
			user.PasswordHash = _userManager.HashPassword(user, loginData.Password);
			await _userRepository.UpdateUserAsync(user);
		}

		UserDto userDto = RegistationHelper.MapUserToDto(user)!;
		LoginTokens? loginTokens = await _tokenService.GenerateTokens(user);

		if (loginTokens is null)
			return LoginErrors.Other;

		UserTokensPair userTokensPair = new()
		{
			User = userDto,
			Tokens = loginTokens
		};

		return userTokensPair;
	}

	public async Task<InfoResult<LoginTokens?, RefreshAccessTokenErrors>> RefreshAccessToken(string accessToken, string refreshToken)
	{
		LoginTokens? loginTokens = await _tokenService.RefreshToken(accessToken, refreshToken);
		string userId = _tokenService.GetUserIdFromToken(accessToken);
		User? user = await _userRepository.GetUserByIdAsync(userId);

		if (user is null)
			return RefreshAccessTokenErrors.UserNotFound;

		if (loginTokens is null)
			return RefreshAccessTokenErrors.InvalidToken;

		return loginTokens;
	}

	public async Task<Result<LogoutErrors>> Logout(string accessToken)
	{
		string userId = _tokenService.GetUserIdFromToken(accessToken);
		User? user = await _userRepository.GetUserByIdAsync(userId);

		if (user is null)
			return LogoutErrors.InvalidToken;

		bool tokensDeleted = await _refreshTokenRepository.DeleteUserTokensAsync(user.Id);

		if (!tokensDeleted)
			return LogoutErrors.Other;

		return new Result<LogoutErrors>() { Error = null };
	}
}
