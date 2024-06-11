using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ProfileImageService;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IProfileImageService
{
	Task<InfoResult<ImageDto, UploadImageError>> UploadImage(UploadImageDto uploadImage, string userId);
}
