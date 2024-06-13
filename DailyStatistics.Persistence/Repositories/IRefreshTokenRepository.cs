using DailyStatistics.Model;

namespace DailyStatistics.Persistence.Repositories;

public interface IRefreshTokenRepository
{
	Task<RefreshToken?> GetTokenAsync(string userId, string token);
	Task<RefreshToken?> AddAsync(RefreshToken refreshToken);
	Task<bool> DeleteAsync(RefreshToken refreshToken);
	Task<bool> DeleteUserTokensAsync(string userId);
	Task DeleteExpired();
}
