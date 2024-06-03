using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Helpers;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.UserService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Persistence.Models;
using DailyStatistics.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Application.Services;

public sealed class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly ITokenService _tokenService;
	private readonly IUserManagerFacade _userManager;

	public UserService(IUserRepository userRepository, IUserManagerFacade userManager, ITokenService tokenService)
	{
		_userRepository = userRepository;
		_userManager = userManager;
		_tokenService = tokenService;
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
}
