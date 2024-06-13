using Helper = DailyStatistics.Application.Helpers.DayRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.DayRecordHelper;

internal class MapTupleToPairTests
{
	[Test]
	public void ShouldMapTupleToPair()
	{
		// Arrange
		var tuple = (new DateOnly(2022, 1, 1), new DateOnly(2022, 1, 2));

		// Act
		var result = Helper.MapTupleToPair(tuple);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.FirstDay.Year, Is.EqualTo(2022));
		Assert.That(result.FirstDay.Month, Is.EqualTo(1));
		Assert.That(result.FirstDay.Day, Is.EqualTo(1));
		Assert.That(result.LastDay.Year, Is.EqualTo(2022));
		Assert.That(result.LastDay.Month, Is.EqualTo(1));
		Assert.That(result.LastDay.Day, Is.EqualTo(2));
	}
}
