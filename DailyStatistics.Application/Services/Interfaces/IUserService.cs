using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.UserService;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IUserService
{
	Task<InfoResult<UserDto?, RegistrationErrors>> CreateAsync(UserRegistrationData registrationData);
	Task<InfoResult<(LoginTokens, UserDto)?, LoginErrors>> LoginAsync(UserLoginData loginData);
}
