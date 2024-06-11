using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DailyStatistics.Application.Helpers;

internal static class ImageHelper
{
	internal static bool IsValidImageExtension(string extension, string[] allowedExtensions)
	{
		return allowedExtensions.Contains(extension);
	}

	internal static void CreateThumbnail(int lnWidth, int lnHeight, byte[] bytes, string outputFilename)
	{
		using (Image image = Image.Load(bytes))
		{
			var ratioX = (double)lnWidth / image.Width;
			var ratioY = (double)lnHeight / image.Height;
			var ratio = Math.Min(ratioX, ratioY);

			var newWidth = (int)(image.Width * ratio);
			var newHeight = (int)(image.Height * ratio);

			image.Mutate(x => x.Resize(newWidth, newHeight));
			image.Save(outputFilename);
		}
	}
}
