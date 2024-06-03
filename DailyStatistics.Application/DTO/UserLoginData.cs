namespace DailyStatistics.Application.DTO;

public class UserLoginData
{
	public string UserNameOrEmail { get; set; } = default!;
	public string Password { get; set; } = default!;
}
