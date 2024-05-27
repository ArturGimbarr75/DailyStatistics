﻿using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Persistence.Repositories.EF;

public class UserRepository : IUserRepository
{
	private readonly ApplicationDbContext _context;

	public UserRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public Task<User> AddUserAsync(User user)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteUserAsync(string userId)
	{
		throw new NotImplementedException();
	}

	public Task<User> GetUserByEmailAsync(string email)
	{
		throw new NotImplementedException();
	}

	public Task<User> GetUserByIdAsync(string userId)
	{
		throw new NotImplementedException();
	}

	public Task<User> UpdateUserAsync(User user)
	{
		throw new NotImplementedException();
	}
}