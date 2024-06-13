using SixLabors.ImageSharp;
using Helper = DailyStatistics.Application.Helpers.ImageHelper;

namespace DailyStatistics.Application.Tests.Helpers.ImageHelper;

internal class CreateThumbnailTests
{
	private const string TEST_FILE = "../../../Helpers/ImageHelper/Files/TestImage.png";
	private const string OUTPUT_FILE = "../../../Helpers/ImageHelper/Files/TestImageOutput.png";

	[Test]
	public void ShouldCreateThumbnail()
	{
		// Arrange
		int lnWidth = 10;
		int lnHeight = 10;
		byte[] bytes = File.ReadAllBytes(TEST_FILE);

		// Act
		Helper.CreateThumbnail(lnWidth, lnHeight, bytes, OUTPUT_FILE);

		// Assert
		Assert.That(File.Exists(OUTPUT_FILE), Is.True);
		using (Image image = Image.Load(OUTPUT_FILE))
		{
			Assert.That(image.Width, Is.LessThanOrEqualTo(lnWidth));
			Assert.That(image.Height, Is.LessThanOrEqualTo(lnHeight));
		}
	}

	[Test]
	public void ThrowsArgumentNullException_WhenBytesAreEmpty()
	{
		// Arrange
		int lnWidth = 10;
		int lnHeight = 10;
		byte[] bytes = Array.Empty<byte>();

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => Helper.CreateThumbnail(lnWidth, lnHeight, bytes, OUTPUT_FILE));
	}

	[Test]
	public void ThrowsArgumentNullException_WhenBytesAreNull()
	{
		// Arrange
		int lnWidth = 10;
		int lnHeight = 10;
		byte[] bytes = null!;

		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => Helper.CreateThumbnail(lnWidth, lnHeight, bytes, OUTPUT_FILE));
	}

	[TearDown]
	public void TearDown()
	{
		if (File.Exists(OUTPUT_FILE))
			File.Delete(OUTPUT_FILE);
	}
}