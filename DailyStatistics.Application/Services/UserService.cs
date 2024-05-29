using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Helpers;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.UserService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Persistence.Models;
using DailyStatistics.Persistence.Repositories;

namespace DailyStatistics.Application.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
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
}
