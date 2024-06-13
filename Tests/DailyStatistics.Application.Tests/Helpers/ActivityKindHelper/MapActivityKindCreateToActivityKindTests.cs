using DailyStatistics.DTO.ActivityKind;
using DailyStatistics.Persistence.Models;
using Helper = DailyStatistics.Application.Helpers.ActivityKindHelper;

namespace DailyStatistics.Application.Tests.Helpers.ActivityKindHelper;

internal class MapActivityKindCreateToActivityKindTests
{
	[Test]
	public void ShouldMapActivityKindCreateToActivityKind()
	{
		// Arrange
		ActivityKindCreate activityKindCreate = new()
		{
			Name = "Test",
			Description = "Test"
		};
		string userId = "Test";

		// Act
		var result = Helper.MapActivityKindCreateToActivityKind(activityKindCreate, userId);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf<TrackingActivityKind>());
		Assert.That(result.Name, Is.EqualTo(activityKindCreate.Name));
		Assert.That(result.UserId, Is.EqualTo(userId));
		Assert.That(result.Description, Is.EqualTo(activityKindCreate.Description));
	}

	[Test]
	public void ShouldMapActivityKindCreateToActivityKind_WhenUserIdIsNull()
	{
		// Arrange
		ActivityKindCreate activityKindCreate = new()
		{
			Name = "Test",
			Description = "Test"
		};
		string userId = null!;

		// Act
		var result = Helper.MapActivityKindCreateToActivityKind(activityKindCreate, userId);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf<TrackingActivityKind>());
		Assert.That(result.Name, Is.EqualTo(activityKindCreate.Name));
		Assert.That(result.UserId, Is.EqualTo(userId));
		Assert.That(result.Description, Is.EqualTo(activityKindCreate.Description));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenActivityKindCreateIsNull()
	{
		// Arrange
		ActivityKindCreate activityKindCreate = null!;
		string userId = "Test";

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapActivityKindCreateToActivityKind(activityKindCreate, userId));
	}
}
