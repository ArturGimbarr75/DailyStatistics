using DailyStatistics.DTO.ActivityKind;
using DailyStatistics.Model;
using Helper = DailyStatistics.Application.Helpers.ActivityKindHelper;

namespace DailyStatistics.Application.Tests.Helpers.ActivityKindHelper;

internal class MapActivityKindDtoToActivityKindTests
{
	[Test]
	public void ShouldMapActivityKindDtoToActivityKind()
	{
		// Arrange
		ActivityKindDto activityKindDto = new()
		{
			Id = Guid.NewGuid(),
			Name = "Test",
			UserId = "Test",
			Description = "Test"
		};

		// Act
		var result = Helper.MapActivityKindDtoToActivityKind(activityKindDto);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf<TrackingActivityKind>());
		Assert.That(result.Id, Is.EqualTo(activityKindDto.Id));
		Assert.That(result.Name, Is.EqualTo(activityKindDto.Name));
		Assert.That(result.UserId, Is.EqualTo(activityKindDto.UserId));
		Assert.That(result.Description, Is.EqualTo(activityKindDto.Description));
	}

	[Test]
	public void ShouldThrowArgumentNullException_WhenActivityKindDtoIsNull()
	{
		// Arrange
		ActivityKindDto activityKindDto = null!;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapActivityKindDtoToActivityKind(activityKindDto));
	}

	[Test]
	public void ShouldMapActivityKindDtoToActivityKind_WhenActivityKindDtoIdIsEmpty()
	{
		// Arrange
		ActivityKindDto activityKindDto = new()
		{
			Id = Guid.Empty,
			Name = "Test",
			UserId = "Test",
			Description = "Test"
		};

		// Act
		var result = Helper.MapActivityKindDtoToActivityKind(activityKindDto);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf<TrackingActivityKind>());
		Assert.That(result.Id, Is.EqualTo(activityKindDto.Id));
		Assert.That(result.Name, Is.EqualTo(activityKindDto.Name));
		Assert.That(result.UserId, Is.EqualTo(activityKindDto.UserId));
		Assert.That(result.Description, Is.EqualTo(activityKindDto.Description));
	}
}
