using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence.Repositories.EF;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
	private readonly ApplicationDbContext _context;

	public RefreshTokenRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<RefreshToken?> AddAsync(RefreshToken refreshToken)
	{
		await _context.RefreshTokens.AddAsync(refreshToken);
		await _context.SaveChangesAsync();

		return refreshToken;
	}

	public async Task<bool> DeleteAsync(RefreshToken refreshToken)
	{
		RefreshToken? foundToken = await _context.RefreshTokens.Where(t => t.UserId == refreshToken.UserId && t.Token == refreshToken.Token).FirstOrDefaultAsync();

		if (foundToken is null)
			return false;

		_context.RefreshTokens.Remove(refreshToken);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<RefreshToken?> GetTokenAsync(string userId, string token)
	{
		return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId && t.Token == token);
	}

	public async Task DeleteExpired()
	{
		// TODO: Check for memory leaks
		_context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(t => t.Expires < DateTime.UtcNow));
		await _context.SaveChangesAsync();
	}

	public async Task<bool> DeleteUserTokensAsync(string userId)
	{
		_context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(t => t.UserId == userId));
		await _context.SaveChangesAsync();
		return true;
	}
}
