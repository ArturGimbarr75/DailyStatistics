using DailyStatistics.Application.Services;
using DailyStatistics.Application.Services.Interfaces;
using DailyStatistics.DTO.Auth;
using DailyStatistics.Persistence;
using DailyStatistics.Model;
using DailyStatistics.Persistence.Repositories;
using DailyStatistics.Persistence.Repositories.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Application.Tests.Services.UserServiceTests;

internal class CreateAsyncTests
{
	/*
	private IUserService _userService = default!;

	[SetUp]
	public void SetUp()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;
		var dbContext = new ApplicationDbContext(options);
		var userStore = new UserStore<User>(dbContext);
		var userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);
		var userRepository = new UserRepository(userManager);

		_userService = new UserService(userRepository);
	}

	[Test]
	public async Task ShouldCreateUser()
	{
		// Arrange
		UserRegistrationData userRegistraitionData = new()
		{
			UserName = "Test",
			FirstName = "Test",
			LastName = "Test",
			Email = "test@gmail.com",
			Password = "Test123",
			ConfirmPassword = "Test123",
			PhoneNumber = "123456789"
		};

		// Act
		var result = _userService.CreateAsync(userRegistraitionData);

		// Assert
		Assert.IsTrue(result.Result);
	}
	*/
}
