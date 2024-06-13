using DailyStatistics.DTO.Day;
using Helper = DailyStatistics.Application.Helpers.DayRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.DayRecordHelper;

internal class MapDayDtoToDayRecordTests
{
	[Test]
	public void ShouldMapDayDtoToDayRecord()
	{
		// Arrange
		var dayDto = new DayDto()
		{
			Id = Guid.NewGuid(),
			Date = new Date() { Year = 2022, Month = 1, Day = 1 },
			Description = "Description"
		};
		var userId = "userId";

		// Act
		var result = Helper.MapDayDtoToDayRecord(dayDto, userId);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(dayDto.Id));
		Assert.That(result.Date, Is.EqualTo(new DateOnly(2022, 1, 1)));
		Assert.That(result.Description, Is.EqualTo(dayDto.Description));
		Assert.That(result.UserId, Is.EqualTo(userId));
	}

	[Test]
	public void ThrowsNullReferenceException_WhenDayDtoIsNull()
	{
		// Arrange
		DayDto dayDto = null!;
		var userId = "userId";

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapDayDtoToDayRecord(dayDto, userId));
	}

	[Test]
	public void ShouldMapDayDtoToDayRecord_WhenUserIdIsNull()
	{
		// Arrange
		var dayDto = new DayDto()
		{
			Id = Guid.NewGuid(),
			Date = new Date() { Year = 2022, Month = 1, Day = 1 },
			Description = "Description"
		};
		string userId = null!;

		// Act
		var result = Helper.MapDayDtoToDayRecord(dayDto, userId);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(dayDto.Id));
		Assert.That(result.Date, Is.EqualTo(new DateOnly(2022, 1, 1)));
		Assert.That(result.Description, Is.EqualTo(dayDto.Description));
		Assert.That(result.UserId, Is.EqualTo(userId));
	}
}
