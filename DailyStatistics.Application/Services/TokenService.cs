using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Persistence.Models;
using DailyStatistics.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DailyStatistics.Application.Services;

public sealed class TokenService : ITokenService
{
	private readonly IUserRepository _userRepository;
	private readonly IRefreshTokenRepository _refreshTokenRepository;

	private readonly string _issuer;
	private readonly string _audience;
	private readonly int _jwtExpiryInterval;
	private readonly int _refreshTokenExpiryInterval;
	private readonly SymmetricSecurityKey _securityKey;

	public TokenService(IConfiguration configuration, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
	{
		string key = configuration["Auth:Jwt:Key"]!;
		_issuer = configuration["Auth:Jwt:Issuer"]!;
		_audience = configuration["Auth:Jwt:Audience"]!;
		_jwtExpiryInterval = int.Parse(configuration["Auth:Jwt:AccessTokenExpiration"]!);
		_refreshTokenExpiryInterval = int.Parse(configuration["Auth:Jwt:RefreshTokenExpiration"]!);

		_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
		_userRepository = userRepository;
		_refreshTokenRepository = refreshTokenRepository;
	}

	public Task<(string jwt, ulong expTimeStamp)> GenerateAccessToken(User user)
	{
		ClaimsIdentity claimsIdentity = new(new[]
		{
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Name, user.UserName!),
			new Claim(ClaimTypes.Email, user.Email!)
		});

		JwtSecurityTokenHandler tokenHandler = new();
		SecurityTokenDescriptor tokenDescriptor = new()
		{
			Audience = _audience,
			Expires = DateTime.UtcNow.AddMinutes(_jwtExpiryInterval),
			Issuer = _issuer,
			SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256),
			Subject = claimsIdentity
		};

		SecurityToken token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
		return Task.FromResult((tokenHandler.WriteToken(token), (ulong)(tokenDescriptor.Expires!.Value - DateTime.UnixEpoch).TotalSeconds));
	}

	public async Task<RefreshToken> GenerateRefreshToken(User user)
	{
		byte[] bytes = new byte[64];
		RandomNumberGenerator.Fill(bytes);

		string encodedToken = Convert.ToBase64String(bytes);
		DateTime createdAt = DateTime.UtcNow;
		DateTime expiresAt = createdAt.AddSeconds(_refreshTokenExpiryInterval);

		RefreshToken refreshToken = new()
		{
			Token = encodedToken,
			UserId = user.Id,
			CreatedAt = createdAt,
			Expires = expiresAt
		};

		await _refreshTokenRepository.AddAsync(refreshToken);

		return refreshToken;
	}

	public async Task<LoginTokens?> RefreshToken(string accessToken, string refreshToken)
	{
		string userId = GetUserIdFromToken(accessToken);

		User? user = await _userRepository.GetUserByIdAsync(userId);
		if (user is null)
			return null;

		RefreshToken? storedRefreshToken = await _refreshTokenRepository.GetTokenAsync(user.Id, refreshToken);

		if (storedRefreshToken is null || storedRefreshToken.Expires < DateTime.UtcNow)
			return null;

		(string jwt, ulong jwtExpTimeStamp) = await GenerateAccessToken(user);
		RefreshToken newRefreshToken = await GenerateRefreshToken(user);

		LoginTokens loginTokens = new()
		{
			AccessToken = jwt,
			AccessTokenExpirationTimeStamp = jwtExpTimeStamp,
			RefreshToken = newRefreshToken.Token,
			RefreshTokenExpirationTimeStamp = (ulong)(newRefreshToken.Expires - DateTime.UnixEpoch).TotalSeconds
		};

		return loginTokens;
	}

	public string GetUserIdFromToken(string accessToken)
	{
		JwtSecurityTokenHandler tokenHandler = new();
		JwtSecurityToken token = tokenHandler.ReadJwtToken(accessToken);
		// TODO: Validate issuer, audience
		return token.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
	}

	public async Task<LoginTokens?> GenerateTokens(User user)
	{
		(string jwt, ulong jwtExpTimeStamp) = await GenerateAccessToken(user);
		RefreshToken refreshToken = await GenerateRefreshToken(user);

		LoginTokens loginTokens = new()
		{
			AccessToken = jwt,
			AccessTokenExpirationTimeStamp = jwtExpTimeStamp,
			RefreshToken = refreshToken.Token,
			RefreshTokenExpirationTimeStamp = (ulong)(refreshToken.Expires - DateTime.UnixEpoch).TotalSeconds
		};

		return loginTokens;
	}
}
