
using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence.Repositories.EF;

public sealed class TrackingActivityKindRepository : ITrackingActivityKindRepository
{
	private readonly ApplicationDbContext _context;

	public TrackingActivityKindRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<TrackingActivityKind?> AddAsync(TrackingActivityKind trackingActivityKind)
	{
		await _context.ActivityKinds.AddAsync(trackingActivityKind);
		await _context.SaveChangesAsync();
		return trackingActivityKind;
	}

	public async Task<bool> DeleteAsync(Guid id)
	{
		TrackingActivityKind? found = await _context.ActivityKinds.FindAsync(id);

		if (found is null)
			return false;

		_context.ActivityKinds.Remove(found);
		await _context.SaveChangesAsync();

		return true;
	}

	public Task<bool> ExistsWithNamesAsync(string userId, string name)
	{
		return Task.FromResult(_context.ActivityKinds.Any(x => x.UserId == userId && x.Name == name));
	}

	public async Task<IEnumerable<TrackingActivityKind>> GetAllOfUserAsync(string userId)
	{
		return await _context.ActivityKinds.Where(x => x.UserId == userId).ToListAsync();
	}

	public Task<TrackingActivityKind?> GetAsync(Guid id)
	{
		return _context.ActivityKinds.FindAsync(id).AsTask();
	}

	public async Task<TrackingActivityKind?> UpdateAsync(TrackingActivityKind trackingActivityKind)
	{
		TrackingActivityKind? found = _context.ActivityKinds.Find(trackingActivityKind.Id);

		if (found is null)
			return null;

		_context.Entry(found).CurrentValues.SetValues(trackingActivityKind);
		await _context.SaveChangesAsync();

		return found;
	}

	public Task<bool> UserOwnsTrackingActivityKind(string userId, Guid trackingActivityKindId)
	{
		return Task.FromResult(_context.ActivityKinds.Any(x => x.UserId == userId && x.Id == trackingActivityKindId));
	}
}
