using DailyStatistics.Application.DTO;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Services.Interfaces;

public interface ITokenService
{
	Task<(string jwt, ulong expTimeStamp)> GenerateAccessToken(User user);
	Task<RefreshToken> GenerateRefreshToken(User user);
	Task<LoginTokens?> RefreshToken(string accessToken, string refreshToken);
	Task<LoginTokens?> GenerateTokens(User user);
	string? GetUserIdFromToken(string accessToken);
}
