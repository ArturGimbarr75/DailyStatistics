namespace DailyStatistics.Application.DTO;

public sealed class UserLoginData
{
	public string UserNameOrEmail { get; set; } = default!;
	public string Password { get; set; } = default!;
}
