using DailyStatistics.DTO.ActivityKind;
using DailyStatistics.Model;
using Helper = DailyStatistics.Application.Helpers.ActivityKindHelper;

namespace DailyStatistics.Application.Tests.Helpers.ActivityKindHelper;

internal class MapActivityKindToDtoTests
{
	[Test]
	public void ShouldMapActivityKindToDto()
	{
		// Arrange
		TrackingActivityKind activityKind = new()
		{
			Id = Guid.NewGuid(),
			Name = "Test",
			UserId = "Test",
			Description = "Test"
		};

		// Act
		var result = Helper.MapActivityKindToDto(activityKind);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf<ActivityKindDto>());
		Assert.That(result.Id, Is.EqualTo(activityKind.Id));
		Assert.That(result.Name, Is.EqualTo(activityKind.Name));
		Assert.That(result.UserId, Is.EqualTo(activityKind.UserId));
		Assert.That(result.Description, Is.EqualTo(activityKind.Description));
	}

	[Test]
	public void ShouldThrowArgumentNullException_WhenActivityKindIsNull()
	{
		// Arrange
		TrackingActivityKind activityKind = null!;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapActivityKindToDto(activityKind));
	}

	[Test]
	public void ShouldMapActivityKindToDto_WhenActivityKindIdIsEmpty()
	{
		// Arrange
		TrackingActivityKind activityKind = new()
		{
			Id = Guid.Empty,
			Name = "Test",
			UserId = "Test",
			Description = "Test"
		};

		// Act
		var result = Helper.MapActivityKindToDto(activityKind);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf<ActivityKindDto>());
		Assert.That(result.Id, Is.EqualTo(activityKind.Id));
		Assert.That(result.Name, Is.EqualTo(activityKind.Name));
		Assert.That(result.UserId, Is.EqualTo(activityKind.UserId));
		Assert.That(result.Description, Is.EqualTo(activityKind.Description));
	}
}
