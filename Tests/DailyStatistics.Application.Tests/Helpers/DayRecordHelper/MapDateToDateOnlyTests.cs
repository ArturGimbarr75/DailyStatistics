using DailyStatistics.DTO.Day;
using Helper = DailyStatistics.Application.Helpers.DayRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.DayRecordHelper;

internal class MapDateToDateOnlyTests
{
	[Test]
	public void ShouldMapDateToDateOnly()
	{
		// Arrange
		var date = new Date() { Year = 2022, Month = 1, Day = 1 };

		// Act
		var result = Helper.MapDateToDateOnly(date);

		// Assert
		Assert.That(result, Is.EqualTo(new DateOnly(2022, 1, 1)));
	}

	[Test]
	public void ThrowsNullReferenceException_WhenDateIsNull()
	{
		// Arrange
		Date date = null!;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapDateToDateOnly(date));
	}
}
