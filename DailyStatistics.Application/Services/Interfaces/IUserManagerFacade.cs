using DailyStatistics.Persistence.Models;
using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IUserManagerFacade
{
	PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
	string HashPassword(User user, string password);
}
