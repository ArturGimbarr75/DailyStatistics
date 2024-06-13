using DailyStatistics.DTO.Image;
using DailyStatistics.Model;

namespace DailyStatistics.Application.Helpers;

public static class ProfileImageHelper
{
	public static ImageDto MapProfileImageToDto(ProfileImage profileImage)
	{
		return new ImageDto
		{
			Id = profileImage.Id,
			UserId = profileImage.UserId!
		};
	}

	public static ProfileImage MapProfileImageDtoToProfileImage(ImageDto imageDto, string path)
	{
		return new ProfileImage
		{
			Id = imageDto.Id,
			UserId = imageDto.UserId,
			ImagePath = path
		};
	}
}