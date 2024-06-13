using DailyStatistics.Model;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence.Repositories.EF;

public class ProfileImageRepository : IProfileImageRepository
{
	private readonly ApplicationDbContext _context;

	public ProfileImageRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<ProfileImage?> AddProfileImageAsync(ProfileImage profileImage)
	{
		await _context.ProfileImages.AddAsync(profileImage);
		await _context.SaveChangesAsync();

		return profileImage;
	}

	public async Task DeleteAllUserProfileImagesAsync(string userId)
	{
		IEnumerable<ProfileImage> profileImages = _context.ProfileImages.Where(t => t.UserId == userId);
		_context.ProfileImages.RemoveRange(profileImages);
		await _context.SaveChangesAsync();
	}

	public async Task<bool> DeleteProfileImageAsync(Guid profileImageId)
	{
		ProfileImage? profileImage = _context.ProfileImages.Find(profileImageId);

		if (profileImage is null)
			return false;

		_context.ProfileImages.Remove(profileImage);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<ProfileImage?> GetProfileImageByIdAsync(Guid profileImageId)
	{
		return await _context.ProfileImages.FindAsync(profileImageId);
	}

	public async Task<ProfileImage?> GetProfileImageByUserIdAsync(string userId)
	{
		Guid? id = (await _context.Users.FindAsync(userId))?.SelectedProfileImageId;
		return id is null ? null : await _context.ProfileImages.FirstAsync(t => t.Id == id);
	}

	public async Task<IEnumerable<ProfileImage>> GetProfileImagesByUserIdAsync(string userId)
	{
		return await _context.ProfileImages.Where(t => t.UserId == userId).ToListAsync();
	}

	public async Task<ProfileImage?> GetSelectedProfileImage(string userId)
	{
		return (await _context.Users.FindAsync(userId))?.SelectedProfileImage;
	}

	public Task<bool> UserOwnsImage(string userId, Guid profileImageId)
	{
		return _context.ProfileImages.AnyAsync(t => t.UserId == userId && t.Id == profileImageId);
	}
}
