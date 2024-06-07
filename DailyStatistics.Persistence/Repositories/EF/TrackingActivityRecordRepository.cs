using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence.Repositories.EF;

public class TrackingActivityRecordRepository : ITrackingActivityRecordRepository
{
	private readonly ApplicationDbContext _context;

	public TrackingActivityRecordRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<TrackingActivityRecord?> AddTrackingActivityRecordAsync(TrackingActivityRecord trackingActivityRecord)
	{
		await _context.ActivityRecords.AddAsync(trackingActivityRecord);
		await _context.SaveChangesAsync();

		return trackingActivityRecord;
	}

	public async Task<bool> DayHasRecordWithKind(DateOnly date, string userId, Guid kindId)
	{
		return await _context.ActivityRecords.AnyAsync(ar => ar.DayRecord.Date == date
														  && ar.DayRecord.UserId == userId
														  && ar.TrackingActivityKindId == kindId);
	}

	public async Task<bool> DeleteTrackingActivityRecordAsync(Guid id)
	{
		TrackingActivityRecord? foundRecord = _context.ActivityRecords.Find(id);

		if (foundRecord is null)
			return false;

		_context.ActivityRecords.Remove(foundRecord);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<TrackingActivityRecord?> GetTrackingActivityRecordAsync(Guid id)
	{
		return await _context.ActivityRecords.FindAsync(id);
	}

	public async Task<IEnumerable<TrackingActivityRecord>> GetTrackingActivityRecordsFromDayAsync(DateOnly date, string userId)
	{
		return await _context.ActivityRecords
			.Where(ar => ar.DayRecord.Date == date && ar.DayRecord.UserId == userId)
			.ToListAsync();
	}

	public async Task<TrackingActivityRecord?> UpdateTrackingActivityRecordAsync(TrackingActivityRecord trackingActivityRecord)
	{
		TrackingActivityRecord? foundRecord = _context.ActivityRecords.Find(trackingActivityRecord.Id);

		if (foundRecord is null)
			return null;

		_context.Entry(foundRecord).CurrentValues.SetValues(trackingActivityRecord);
		_context.Update(foundRecord);
		await _context.SaveChangesAsync();

		return foundRecord;
    }

	public Task<bool> UserOwnsRecord(string userId, Guid recordId)
	{
		return _context.ActivityRecords.AnyAsync(ar => ar.DayRecord.UserId == userId && ar.Id == recordId);
	}
}
