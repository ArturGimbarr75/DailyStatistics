using Microsoft.AspNetCore.Http;

namespace DailyStatistics.Application.DTO;

public sealed class UploadImageDto
{ 
	public IFormFile Image { get; set; } = default!;
}
