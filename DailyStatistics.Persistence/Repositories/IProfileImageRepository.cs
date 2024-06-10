using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Persistence.Repositories;

public interface IProfileImageRepository
{
	Task<ProfileImage?> GetSelectedProfileImage(string userId);
	Task<ProfileImage?> GetProfileImageByIdAsync(string profileImageId);
	Task<ProfileImage?> GetProfileImageByUserIdAsync(string userId);
	Task<IEnumerable<ProfileImage>> GetProfileImagesByUserIdAsync(string userId);
	Task<ProfileImage?> AddProfileImageAsync(ProfileImage profileImage);
	Task<bool> DeleteProfileImageAsync(string profileImageId);
	Task DeleteAllUserProfileImagesAsync(string userId);
}