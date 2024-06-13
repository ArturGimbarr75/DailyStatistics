namespace DailyStatistics.DTO.Auth;

public sealed class TokensPair
{
	public string AccessToken { get; set; } = default!;
	public string RefreshToken { get; set; } = default!;
}