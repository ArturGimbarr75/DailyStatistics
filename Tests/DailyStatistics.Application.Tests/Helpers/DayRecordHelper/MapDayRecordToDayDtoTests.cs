using DailyStatistics.Persistence.Models;
using Helper = DailyStatistics.Application.Helpers.DayRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.DayRecordHelper;

internal class MapDayRecordToDayDtoTests
{
	[Test]
	public void ShouldMapDayRecordToDayDto()
	{
		// Arrange
		var dayRecord = new DayRecord()
		{
			Id = Guid.NewGuid(),
			Date = new DateOnly(2022, 1, 1),
			Description = "Description"
		};

		// Act
		var result = Helper.MapDayRecordToDayDto(dayRecord);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(dayRecord.Id));
		Assert.That(result.Date.Year, Is.EqualTo(2022));
		Assert.That(result.Date.Month, Is.EqualTo(1));
		Assert.That(result.Date.Day, Is.EqualTo(1));
		Assert.That(result.Description, Is.EqualTo(dayRecord.Description));
	}

	[Test]
	public void ThrowsNullReferenceException_WhenDayRecordIsNull()
	{
		// Arrange
		DayRecord dayRecord = null!;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapDayRecordToDayDto(dayRecord));
	}
}
