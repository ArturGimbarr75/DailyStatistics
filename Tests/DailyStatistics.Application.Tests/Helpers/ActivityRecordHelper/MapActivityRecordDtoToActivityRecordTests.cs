using DailyStatistics.DTO.Record;
using Helper = DailyStatistics.Application.Helpers.ActivityRecordHelper;

namespace DailyStatistics.Application.Tests.Helpers.ActivityRecordHelper;

internal class MapActivityRecordDtoToActivityRecordTests
{
	[Test]
	public void ShouldMapActivityRecordDtoToActivityRecord()
	{
		// Arrange
		var activityRecordDto = new ActivityRecordDto
		{
			Id = Guid.NewGuid(),
			Amount = 10,
			DayRecordId = Guid.NewGuid(),
			ActivityKindId = Guid.NewGuid()			
		};

		// Act
		var result = Helper.MapActivityRecordDtoToActivityRecord(activityRecordDto);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(activityRecordDto.Id));
		Assert.That(result.Amount, Is.EqualTo(activityRecordDto.Amount));
		Assert.That(result.DayRecordId, Is.EqualTo(activityRecordDto.DayRecordId));
		Assert.That(result.TrackingActivityKindId, Is.EqualTo(activityRecordDto.ActivityKindId));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenActivityRecordDtoIsNull()
	{
		// Arrange
		ActivityRecordDto activityRecordDto = null!;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapActivityRecordDtoToActivityRecord(activityRecordDto));
	}

	[Test]
	public void ShouldMapActivityRecordDtoToActivityRecord_WhenActivityRecordDtoIsEmpty()
	{
		// Arrange
		var activityRecordDto = new ActivityRecordDto();

		// Act
		var result = Helper.MapActivityRecordDtoToActivityRecord(activityRecordDto);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(Guid.Empty));
		Assert.That(result.Amount, Is.EqualTo(0));
		Assert.That(result.DayRecordId, Is.EqualTo(Guid.Empty));
		Assert.That(result.TrackingActivityKindId, Is.EqualTo(Guid.Empty));
	}
}
