using DailyStatistics.DTO.Auth;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Helpers;

public static class RegistationHelper
{
	public static User MapRegistrationData(UserRegistrationData registrationData)
	{
		return new User
		{
			Email = registrationData.Email,
			UserName = registrationData.UserName,
			Name = registrationData.FirstName,
			Surname = registrationData.LastName
		};
	}

	public static UserDto MapUserToDto(User user)
	{
		return new UserDto
		{
			Id = user.Id,
			Email = user.Email!,
			UserName = user.UserName!,
			Name = user.Name,
			Surname = user.Surname,
			PhoneNumber = user.PhoneNumber
		};
	}	
}
