﻿namespace DailyStatistics.Application.DTO;

public class UserDto
{
	public string Id { get; set; } = default!;
	public string Email { get; set; } = default!;
	public string UserName { get; set; } = default!;
	public string Name { get; set; } = default!;
	public string Surname { get; set; } = default!;
	public string? PhoneNumber { get; set; } = default!;
}