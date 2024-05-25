using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Persistence.Models;

public sealed class User : IdentityUser
{
	public string Name { get; set; } = default!;
	public string Surname { get; set; } = default!;
}
