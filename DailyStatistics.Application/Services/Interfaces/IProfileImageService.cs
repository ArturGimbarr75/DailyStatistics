using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ProfileImageService;

namespace DailyStatistics.Application.Services.Interfaces;

public interface IProfileImageService
{
	Task<InfoResult<ImageDto?, UploadImageError>> UploadImage(UploadImageDto uploadImage, string userId);
	Task<Result<ImageDto?, GetImageError>> GetProfileImage(string userId);
	Task<Result<(ImageDto dto, string path)?, GetImageError>> GetImage(string userId, Guid imageId);
	Task<Result<IEnumerable<ImageDto>, GetImageError>> GetImages(string userId);
	Task<Result<DeleteImageError>> DeleteImage(string userId, Guid imageId);
	Task<Result<SetProfileImageError>> SetProfileImage(string userId, Guid imageId);
}
