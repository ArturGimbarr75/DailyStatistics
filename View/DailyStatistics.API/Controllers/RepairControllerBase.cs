using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DailyStatistics.API.Controllers;

public abstract class RepairControllerBase : Controller
{
	public string? UserId => User.Identities.FirstOrDefault()?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
}
