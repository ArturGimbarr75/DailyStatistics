using DailyStatistics.Persistence.Models;
using Helper = DailyStatistics.Application.Helpers.ActivityRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.ActivityRecordHelper;

internal class MapActivityRecordToActivityRecordDtoTests
{
	[Test]
	public void ShouldMapActivityRecordToActivityRecordDto()
	{
		// Arrange
		var activityRecord = new TrackingActivityRecord
		{
			Id = Guid.NewGuid(),
			Amount = 10,
			DayRecordId = Guid.NewGuid(),
			TrackingActivityKindId = Guid.NewGuid()			
		};

		// Act
		var result = Helper.MapActivityRecordToActivityRecordDto(activityRecord);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(activityRecord.Id));
		Assert.That(result.Amount, Is.EqualTo(activityRecord.Amount));
		Assert.That(result.DayRecordId, Is.EqualTo(activityRecord.DayRecordId));
		Assert.That(result.ActivityKindId, Is.EqualTo(activityRecord.TrackingActivityKindId));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenActivityRecordIsNull()
	{
		// Arrange
		TrackingActivityRecord activityRecord = null!;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapActivityRecordToActivityRecordDto(activityRecord));
	}

	[Test]
	public void ShouldMapActivityRecordToActivityRecordDto_WhenActivityRecordIsEmpty()
	{
		// Arrange
		var activityRecord = new TrackingActivityRecord();

		// Act
		var result = Helper.MapActivityRecordToActivityRecordDto(activityRecord);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(Guid.Empty));
		Assert.That(result.Amount, Is.EqualTo(0));
		Assert.That(result.DayRecordId, Is.EqualTo(Guid.Empty));
		Assert.That(result.ActivityKindId, Is.EqualTo(Guid.Empty));
	}
}
