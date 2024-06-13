using DailyStatistics.DTO.Auth;
using DailyStatistics.Model;
using Helper = DailyStatistics.Application.Helpers.RegistationHelper;

namespace DailyStatistics.Application.Tests.Helpers.RegistationHelper;

internal class MapRegistrationDataTests
{
	[Test]
	public void ShouldMapRegistrationData()
	{
		// Arrange
		var registrationData = new UserRegistrationData()
		{
			Email = "test@gmail.com",
			UserName = "test",
			FirstName = "Test",
			LastName = "Test"
		};

		// Act
		var result = Helper.MapRegistrationData(registrationData);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Email, Is.EqualTo(registrationData.Email));
		Assert.That(result.UserName, Is.EqualTo(registrationData.UserName));
		Assert.That(result.Name, Is.EqualTo(registrationData.FirstName));
		Assert.That(result.Surname, Is.EqualTo(registrationData.LastName));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenRegistrationDataIsNull()
	{
		// Arrange
		UserRegistrationData? registrationData = null;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapRegistrationData(registrationData!));
	}
}

internal class MapUserToDtoTests
{
	[Test]
	public void ShouldMapUserToDto()
	{
		// Arrange
		var user = new User()
		{
			Id = "1",
			Email = "test@gmail.com",
			UserName = "test",
			Name = "Test",
			Surname = "Test",
			PhoneNumber = "123456789",
			PasswordHash = "password"
		};

		// Act
		var result = Helper.MapUserToDto(user);

		// Assert
		Assert.That(result, Is.Not.Null);
		Assert.That(result.Id, Is.EqualTo(user.Id));
		Assert.That(result.Email, Is.EqualTo(user.Email));
		Assert.That(result.UserName, Is.EqualTo(user.UserName));
		Assert.That(result.Name, Is.EqualTo(user.Name));
		Assert.That(result.Surname, Is.EqualTo(user.Surname));
		Assert.That(result.PhoneNumber, Is.EqualTo(user.PhoneNumber));
	}

	[Test]
	public void ShouldThrowNullReferenceException_WhenUserIsNull()
	{
		// Arrange
		User? user = null;

		// Act & Assert
		Assert.Throws<NullReferenceException>(() => Helper.MapUserToDto(user!));
	}
}
