using DailyStatistics.DTO.Day;
using Helper = DailyStatistics.Application.Helpers.DayRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.DayRecordHelper;

internal class MapDayCreateToDayRecordTests
{
	[Test]
	public void ShouldMapDayDtoToDayRecord()
	{
		// Arrange
		var dayCreate = new DayCreate()
		{
			Date = new Date() { Year = 2022, Month = 1, Day = 1 },
			Description = "Description"
		};
		var userId = "userId";

		// Act
		var result = Helper.MapDayDtoToDayRecord(dayCreate, userId);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Date, Is.EqualTo(new DateOnly(2022, 1, 1)));
		Assert.That(result.Description, Is.EqualTo(dayCreate.Description));
		Assert.That(result.UserId, Is.EqualTo(userId));
	}

	[Test]
	public void ThrowsNullReferenceException_WhenDayCreateIsNull()
	{
		// Arrange
		DayCreate dayCreate = null!;
		var userId = "userId";

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapDayDtoToDayRecord(dayCreate, userId));
	}

	[Test]
	public void ShouldMapDayDtoToDayRecord_WhenUserIdIsNull()
	{
		// Arrange
		var dayCreate = new DayCreate()
		{
			Date = new Date() { Year = 2022, Month = 1, Day = 1 },
			Description = "Description"
		};
		string userId = null!;

		// Act
		var result = Helper.MapDayDtoToDayRecord(dayCreate, userId);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Date, Is.EqualTo(new DateOnly(2022, 1, 1)));
		Assert.That(result.Description, Is.EqualTo(dayCreate.Description));
		Assert.That(result.UserId, Is.EqualTo(userId));
	}
}
