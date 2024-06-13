using DailyStatistics.Persistence.Models;
using Helper = DailyStatistics.Application.Helpers.ProfileImageHelper;

namespace DailyStatistics.Application.Tests.Helpers.ProfileImageHelper;

internal class MapProfileImageToDtoTests
{
	[Test]
	public void ShouldMapProfileImageToDto()
	{
		// Arrange
		var profileImage = new ProfileImage()
		{
			Id = Guid.NewGuid(),
			UserId = "1"
		};

		// Act
		var result = Helper.MapProfileImageToDto(profileImage);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(profileImage.Id));
		Assert.That(result.UserId, Is.EqualTo(profileImage.UserId));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenProfileImageIsNull()
	{
		// Arrange
		ProfileImage? profileImage = null;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapProfileImageToDto(profileImage!));
	}
}
