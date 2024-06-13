using DailyStatistics.Model;
using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Persistence.Repositories.EF;

public class UserRepository : IUserRepository
{
	private readonly UserManager<User> _userManager;

	public UserRepository(UserManager<User> userManager)
	{
		_userManager = userManager;
	}

	public async Task<(User?, IdentityResult)> AddUserAsync(User user, string password)
	{
		var identityResult = await _userManager.CreateAsync(user, password);
		return (await _userManager.FindByIdAsync(user.Id), identityResult);
	}

	public async Task<bool> DeleteUserAsync(string userId)
	{
		var foundUser = await _userManager.FindByIdAsync(userId);
		if (foundUser is null)
			return false;

		var result = await _userManager.DeleteAsync(foundUser);
		return result.Succeeded;
	}

	public Task<User?> GetUserByEmailAsync(string email)
	{
		return _userManager.FindByEmailAsync(email);
	}

	public Task<User?> GetUserByIdAsync(string userId)
	{
		return _userManager.FindByIdAsync(userId);
	}

	public Task<User?> GetUserByUserNameAsync(string userName)
	{
		return _userManager.FindByNameAsync(userName);
	}

	public async Task<User?> UpdateUserAsync(User user)
	{
		var foundUser = await _userManager.FindByIdAsync(user.Id);
		await _userManager.UpdateAsync(user);

		return foundUser;
	}
}
