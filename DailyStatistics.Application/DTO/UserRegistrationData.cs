namespace DailyStatistics.Application.DTO;

public class UserRegistrationData
{
	public string Email { get; set; } = default!;
	public string Password { get; set; } = default!;
	public string ConfirmPassword { get; set; } = default!;
	public string UserName { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public string? PhoneNumber { get; set; } = null;
}
