using DailyStatistics.DTO.Image;
using Helper = DailyStatistics.Application.Helpers.ProfileImageHelper;

namespace DailyStatistics.Application.Tests.Helpers.ProfileImageHelper;

internal class MapProfileImageDtoToProfileImageTests
{
	[Test]
	public void ShouldMapProfileImageDtoToProfileImage()
	{
		// Arrange
		var imageDto = new ImageDto()
		{
			Id = Guid.NewGuid(),
			UserId = "1"
		};
		string path = "path";

		// Act
		var result = Helper.MapProfileImageDtoToProfileImage(imageDto, path);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(imageDto.Id));
		Assert.That(result.UserId, Is.EqualTo(imageDto.UserId));
		Assert.That(result.ImagePath, Is.EqualTo(path));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenImageDtoIsNull()
	{
		// Arrange
		ImageDto? imageDto = null;
		string path = "path";

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapProfileImageDtoToProfileImage(imageDto!, path));
	}
}
