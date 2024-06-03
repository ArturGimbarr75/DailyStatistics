namespace DailyStatistics.Application.DTO;

public sealed class TokensPair
{
	public string AccessToken { get; set; } = default!;
	public string RefreshToken { get; set; } = default!;
}