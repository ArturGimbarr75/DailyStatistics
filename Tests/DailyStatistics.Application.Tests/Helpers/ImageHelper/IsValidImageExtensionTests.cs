using Helper = DailyStatistics.Application.Helpers.ImageHelper;

namespace DailyStatistics.Application.Tests.Helpers.ImageHelper;

internal class IsValidImageExtensionTests
{
	[Test]
	public void ShouldReturnTrue_WhenExtensionIsAllowed()
	{
		// Arrange
		string extension = ".jpg";
		string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

		// Act
		var result = Helper.IsValidImageExtension(extension, allowedExtensions);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void ShouldReturnFalse_WhenExtensionIsNotAllowed()
	{
		// Arrange
		string extension = ".bmp";
		string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

		// Act
		var result = Helper.IsValidImageExtension(extension, allowedExtensions);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void ShouldReturnFalse_WhenExtensionIsEmpty()
	{
		// Arrange
		string extension = string.Empty;
		string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

		// Act
		var result = Helper.IsValidImageExtension(extension, allowedExtensions);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void ShouldReturnFalse_WhenAllowedExtensionsIsEmpty()
	{
		// Arrange
		string extension = ".jpg";
		string[] allowedExtensions = Array.Empty<string>();

		// Act
		var result = Helper.IsValidImageExtension(extension, allowedExtensions);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void ShouldReturnFalse_WhenExtensionIsNull()
	{
		// Arrange
		string extension = null!;
		string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

		// Act
		var result = Helper.IsValidImageExtension(extension, allowedExtensions);

		// Assert
		Assert.That(result, Is.False);
	}
}
