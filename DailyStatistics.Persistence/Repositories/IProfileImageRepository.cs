using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Persistence.Repositories;

public interface IProfileImageRepository
{
	Task<ProfileImage?> GetSelectedProfileImage(string userId);
	Task<ProfileImage?> GetProfileImageByIdAsync(Guid profileImageId);
	Task<ProfileImage?> GetProfileImageByUserIdAsync(string userId);
	Task<IEnumerable<ProfileImage>> GetProfileImagesByUserIdAsync(string userId);
	Task<ProfileImage?> AddProfileImageAsync(ProfileImage profileImage);
	Task<bool> DeleteProfileImageAsync(Guid profileImageId);
	Task DeleteAllUserProfileImagesAsync(string userId);
	Task<bool> UserOwnsImage(string userId, Guid profileImageId);
}