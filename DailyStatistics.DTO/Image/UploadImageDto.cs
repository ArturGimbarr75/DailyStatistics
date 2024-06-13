using Microsoft.AspNetCore.Http;

namespace DailyStatistics.DTO.Image;

public sealed class UploadImageDto
{ 
	public IFormFile Image { get; set; } = default!;
}
