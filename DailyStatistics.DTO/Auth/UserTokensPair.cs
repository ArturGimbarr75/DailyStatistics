namespace DailyStatistics.DTO.Auth;

public sealed class UserTokensPair
{
	public UserDto User { get; set; } = default!;
	public LoginTokens Tokens { get; set; } = default!;
}
