﻿using DailyStatistics.Persistence.EntityConfigurations;
using DailyStatistics.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence;

public sealed class ApplicationDbContext : IdentityDbContext<User>
{
	public DbSet<DayRecord> DayRecords { get; set; } = default!;
	public DbSet<ProfileImage> ProfileImages { get; set; } = default!;
	public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
	public DbSet<TrackingActivityGroup> ActivityGroups { get; set; } = default!;
	public DbSet<TrackingActivityGroupMember> ActivityGroupMembers { get; set; } = default!;
	public DbSet<TrackingActivityKind> ActivityKinds { get; set; } = default!;
	public DbSet<TrackingActivityRecord> ActivityRecords { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
		
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrackingActivityKindConfiguration).Assembly);
	}
}

