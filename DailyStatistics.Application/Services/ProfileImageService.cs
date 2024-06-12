using DailyStatistics.Application.DTO;
using DailyStatistics.Application.Helpers;
using DailyStatistics.Application.Infrastructure;
using DailyStatistics.Application.Services.Errors.ProfileImageService;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.Persistence.Models;
using DailyStatistics.Persistence.Repositories;
using Microsoft.Extensions.Configuration;

namespace DailyStatistics.Application.Services;

public class ProfileImageService : IProfileImageService
{
	private readonly IProfileImageRepository _profileImageRepository;
	private readonly IUserRepository _userRepository;
	private readonly string _profileImagesPath;
	private readonly string[] _allowedExtensions;
	private readonly int _maxWidth;
	private readonly int _maxHeight;

	public ProfileImageService(IProfileImageRepository profileImageRepository,
							   IConfiguration configuration,
							   IUserRepository userRepository)
	{
		_profileImageRepository = profileImageRepository;
		_userRepository = userRepository;
		_profileImagesPath = configuration["Images:UserProfilePicturesPath"]!;
		_allowedExtensions = configuration.GetRequiredSection("Images:AllowedExtensions").Get<string[]>()!;
		_maxHeight = configuration.GetValue<int>("Images:MaxHeight");
		_maxWidth = configuration.GetValue<int>("Images:MaxWidth");
	}

	public async Task<Result<DeleteImageError>> DeleteImage(string userId, Guid imageId)
	{
		User? user = await _userRepository.GetUserByIdAsync(userId);

		if (user is null)
			return DeleteImageError.UserNotFound;

		if (!await _profileImageRepository.UserOwnsImage(userId, imageId))
			return DeleteImageError.ImageNotFound;

		ProfileImage? profileImage = await _profileImageRepository.GetProfileImageByIdAsync(imageId);
		
		if (profileImage is null)
			return DeleteImageError.ImageNotFound;

		if (File.Exists(profileImage.ImagePath))
			File.Delete(profileImage.ImagePath);

		bool deleted = await _profileImageRepository.DeleteProfileImageAsync(imageId);

		if (!deleted)
			return DeleteImageError.ImageNotDeleted;

		return Result.Ok<DeleteImageError>();
	}

	public async Task<Result<(ImageDto dto, string path)?, GetImageError>> GetImage(string userId, Guid imageId)
	{
		User? user = await _userRepository.GetUserByIdAsync(userId);

		if (user is null)
			return GetImageError.UserNotFound;

		if (!await _profileImageRepository.UserOwnsImage(userId, imageId))
			return GetImageError.ImageNotFound;

		ProfileImage? profileImage = await _profileImageRepository.GetProfileImageByIdAsync(imageId);

		if (profileImage is null)
			return GetImageError.ImageNotFound;

		return (ProfileImageHelper.MapProfileImageToDto(profileImage), profileImage.ImagePath);
	}

	public async Task<Result<IEnumerable<ImageDto>, GetImageError>> GetImages(string userId)
	{
		User? user = await _userRepository.GetUserByIdAsync(userId);

		if (user is null)
			return GetImageError.UserNotFound;

		IEnumerable<ProfileImage> profileImages = await _profileImageRepository.GetProfileImagesByUserIdAsync(userId);

		var images = profileImages.Select(ProfileImageHelper.MapProfileImageToDto);
		var result = Result<IEnumerable<ImageDto>, GetImageError>.Ok(images);
		return result;
	}

	public async Task<Result<ImageDto?, GetImageError>> GetProfileImage(string userId)
	{
		User? user = await _userRepository.GetUserByIdAsync(userId);

		if (user is null)
			return GetImageError.UserNotFound;

		ProfileImage? profileImage = await _profileImageRepository.GetProfileImageByUserIdAsync(userId);

		if (profileImage is null)
			return Result<ImageDto?, GetImageError>.Ok(null);

		return ProfileImageHelper.MapProfileImageToDto(profileImage);
	}

	public async Task<Result<SetProfileImageError>> SetProfileImage(string userId, Guid imageId)
	{
		User? user = _userRepository.GetUserByIdAsync(userId).Result;

		if (user is null)
			return SetProfileImageError.UserNotFound;

		if (!await _profileImageRepository.UserOwnsImage(userId, imageId))
			return SetProfileImageError.ImageNotFound;

		user.SelectedProfileImageId = imageId;
		User? updatedUser = await _userRepository.UpdateUserAsync(user);

		if (updatedUser is null)
			return SetProfileImageError.ImageNotSet;

		return Result.Ok<SetProfileImageError>();
	}

	public async Task<InfoResult<ImageDto?, UploadImageError>> UploadImage(UploadImageDto uploadImage, string userId)
	{
		if (uploadImage.Image.Length == 0)
		{
			string info = "No image uploaded";
			return InfoResult<ImageDto?, UploadImageError>
				.WithInfo(UploadImageError.InvalidImageSize, info);
		}

		string extension = Path.GetExtension(uploadImage.Image.FileName);
		if (!ImageHelper.IsValidImageExtension(extension, _allowedExtensions))
		{
			string[] info =
			{
				"Invalid image extension",
				$"Allowed extensions: ({string.Join(" ", _allowedExtensions)})"
			};

			return InfoResult<ImageDto?, UploadImageError>
				.WithInfo(UploadImageError.InvalidImageExtension, info);
		}

        User? user = await _userRepository.GetUserByIdAsync(userId);
		if (user is null)
			return InfoResult<ImageDto?, UploadImageError>
				.WithInfo(UploadImageError.UserNotFound, "User not found");

        string imageName = $"{Guid.NewGuid()}{extension}";
		string imagePath = Path.Combine(Environment.CurrentDirectory, _profileImagesPath, imageName);

		if (!Directory.Exists(Path.GetDirectoryName(imagePath)))
			Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);

		using MemoryStream memoryStream = new();
		await uploadImage.Image.CopyToAsync(memoryStream);

		byte[] bytes = memoryStream.ToArray();
		ImageHelper.CreateThumbnail(_maxWidth, _maxHeight, bytes, imagePath);

		ProfileImage profileImage = new()
		{
			UserId = userId,
			ImagePath = imagePath
		};

		ProfileImage? addedProfileImage = await _profileImageRepository.AddProfileImageAsync(profileImage);

		if (addedProfileImage is null)
			return InfoResult<ImageDto?, UploadImageError>
				.WithInfo(UploadImageError.ProfileImageNotAdded, "Profile image not added");

		return ProfileImageHelper.MapProfileImageToDto(addedProfileImage);
	}
}
