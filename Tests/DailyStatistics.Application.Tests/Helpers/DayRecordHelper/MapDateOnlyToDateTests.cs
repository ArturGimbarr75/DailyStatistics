using Helper = DailyStatistics.Application.Helpers.DayRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.DayRecordHelper;

internal class MapDateOnlyToDateTests
{
	[Test]
	public void ShouldMapDateOnlyToDate()
	{
		// Arrange
		var dateOnly = new DateOnly(2022, 1, 1);

		// Act
		var result = Helper.MapDateOnlyToDate(dateOnly);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Year, Is.EqualTo(2022));
		Assert.That(result.Month, Is.EqualTo(1));
		Assert.That(result.Day, Is.EqualTo(1));
	}
}
