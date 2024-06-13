using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Model;
using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Application.Services;

public class UserManagerFacade : IUserManagerFacade
{
    private readonly UserManager<User> _userManager;

    public UserManagerFacade(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

	public string HashPassword(User user, string password)
	{
		return _userManager.PasswordHasher.HashPassword(user, password);
	}

	public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
	{
		return _userManager.PasswordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
	}
}
