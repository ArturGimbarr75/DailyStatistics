using DailyStatistics.Application.DTO;
using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Application.Helpers;

internal static class ProfileImageHelper
{
	internal static ImageDto MapProfileImageToDto(ProfileImage profileImage)
	{
		return new ImageDto
		{
			Id = profileImage.Id,
			UserId = profileImage.UserId!
		};
	}
    
	internal static ProfileImage MapProfileImageDtoToProfileImage(ImageDto imageDto, string path)
	{
		return new ProfileImage
		{
			Id = imageDto.Id,
			UserId = imageDto.UserId,
			ImagePath = path
		};
	}
}