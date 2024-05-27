using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	public DbSet<DayRecord> DayRecords { get; set; } = default!;
	public DbSet<TrackingActivityGroup> ActivityGroups { get; set; } = default!;
	public DbSet<TrackingActivityGroupMember> ActivityGroupMembers { get; set; } = default!;
	public DbSet<TrackingActivityKind> ActivityKinds { get; set; } = default!;
	public DbSet<TrackingActivityRecord> ActivityRecords { get; set; }
	public DbSet<User> Users { get; set; } = default!;

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}
}

