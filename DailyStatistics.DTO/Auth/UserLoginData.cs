namespace DailyStatistics.DTO.Auth;

public sealed class UserLoginData
{
	public string UserNameOrEmail { get; set; } = default!;
	public string Password { get; set; } = default!;
}
