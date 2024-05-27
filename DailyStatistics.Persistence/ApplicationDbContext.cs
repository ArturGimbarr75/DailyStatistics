using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence;

public sealed class ApplicationDbContext : DbContext
{
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

