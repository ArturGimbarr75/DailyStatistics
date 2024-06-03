using DailyStatistics.Persistence.Models;
using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Persistence.Repositories;

public interface IUserRepository
{
	Task<(User?, IdentityResult)> AddUserAsync(User user, string password);
	Task<User?> GetUserByIdAsync(string userId);
	Task<User?> GetUserByUserNameAsync(string userName);
	Task<User?> GetUserByEmailAsync(string email);
	Task<bool> DeleteUserAsync(string userId);
	Task<User?> UpdateUserAsync(User user);
}
