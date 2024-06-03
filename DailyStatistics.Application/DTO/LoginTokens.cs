namespace DailyStatistics.Application.DTO;

public sealed class LoginTokens
{
	public string AccessToken { get; set; } = default!;
	public string RefreshToken { get; set; } = default!;
	public ulong AccessTokenExpirationTimeStamp { get; set; }
	public ulong RefreshTokenExpirationTimeStamp { get; set; }
}
