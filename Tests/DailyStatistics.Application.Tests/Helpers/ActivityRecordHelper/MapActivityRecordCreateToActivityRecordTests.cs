using DailyStatistics.DTO.Record;
using Helper = DailyStatistics.Application.Helpers.ActivityRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.ActivityRecordHelper;

internal class MapActivityRecordCreateToActivityRecordTests
{
	[Test]
	public void ShouldMapActivityRecordCreateToActivityRecord()
	{
		// Arrange
		var activityRecordCreate = new ActivityRecordCreate
		{
			Amount = 10,
			DayRecordId = Guid.NewGuid(),
			ActivityKindId = Guid.NewGuid()			
		};

		// Act
		var result = Helper.MapActivityRecordCreateToActivityRecord(activityRecordCreate);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Amount, Is.EqualTo(activityRecordCreate.Amount));
		Assert.That(result.DayRecordId, Is.EqualTo(activityRecordCreate.DayRecordId));
		Assert.That(result.TrackingActivityKindId, Is.EqualTo(activityRecordCreate.ActivityKindId));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenActivityRecordCreateIsNull()
	{
		// Arrange
		ActivityRecordCreate activityRecordCreate = null!;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapActivityRecordCreateToActivityRecord(activityRecordCreate));
	}

	[Test]
	public void ShouldMapActivityRecordCreateToActivityRecord_WhenActivityRecordCreateIsEmpty()
	{
		// Arrange
		var activityRecordCreate = new ActivityRecordCreate();

		// Act
		var result = Helper.MapActivityRecordCreateToActivityRecord(activityRecordCreate);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Amount, Is.EqualTo(0));
		Assert.That(result.DayRecordId, Is.EqualTo(Guid.Empty));
		Assert.That(result.TrackingActivityKindId, Is.EqualTo(Guid.Empty));
	}
}