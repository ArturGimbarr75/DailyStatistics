using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Persistence.Repositories;

public interface IUserRepository
{
	Task<User?> AddUserAsync(User user, string password);
	Task<User?> GetUserByIdAsync(string userId);
	Task<User?> GetUserByEmailAsync(string email);
	Task<bool> DeleteUserAsync(string userId);
	Task<User?> UpdateUserAsync(User user);
}
