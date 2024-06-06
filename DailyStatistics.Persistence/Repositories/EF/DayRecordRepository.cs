using DailyStatistics.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyStatistics.Persistence.Repositories.EF;

public class DayRecordRepository : IDayRecordRepository
{
	private readonly ApplicationDbContext _context;

	public DayRecordRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<DayRecord?> AddDayAsync(DayRecord dayRecord)
	{
		await _context.DayRecords.AddAsync(dayRecord);
		await _context.SaveChangesAsync();

		return dayRecord;
	}

	public async Task<bool> DeleteDayAsync(Guid id)
	{
		DayRecord? foundRecord = _context.DayRecords.Find(id);

		if (foundRecord is null)
			return false;

		_context.DayRecords.Remove(foundRecord);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<IEnumerable<DateOnly>> GetAllDayRecordDates(string userId)
	{
		return await _context.DayRecords
			.Where(dr => dr.UserId == userId)
			.Select(dr => dr.Date)
			.OrderBy(date => date)
			.ToListAsync();
	}

	public Task<DayRecord?> GetDayAsync(DateOnly date, string userId)
	{
		return _context.DayRecords
			.FirstOrDefaultAsync(dr => dr.Date == date && dr.UserId == userId);
	}

	public async Task<IEnumerable<DayRecord>> GetDaysAsync(DateOnly from, DateOnly to, string userId)
	{
		return await _context.DayRecords
			.Where(dr => dr.Date >= from && dr.Date <= to && dr.UserId == userId)
			.ToListAsync();
	}

	public async Task<(DateOnly, DateOnly)?> GetFirstAndLastDayAsync(string userId)
	{
		if (await _context.DayRecords.AnyAsync(dr => dr.UserId == userId))
		{
			DateOnly firstDay = await _context.DayRecords
				.Where(dr => dr.UserId == userId)
				.MinAsync(dr => dr.Date);

			DateOnly lastDay = await _context.DayRecords
				.Where(dr => dr.UserId == userId)
				.MaxAsync(dr => dr.Date);

			return (firstDay, lastDay);
		}

		return null;
	}

	public async Task<DayRecord?> UpdateDayAsync(DayRecord dayRecord)
	{
		DayRecord? foundRecord = await _context.DayRecords.FindAsync(dayRecord.Id);

		if (foundRecord is null)
			return null;

		_context.Entry(foundRecord).CurrentValues.SetValues(dayRecord);
		_context.DayRecords.Update(foundRecord);
		await _context.SaveChangesAsync();

		return foundRecord;
	}

	public Task<bool> UserHasDayRecord(DateOnly date, string userId)
	{
		return _context.DayRecords
			.AnyAsync(dr => dr.Date == date && dr.UserId == userId);
	}

	public Task<bool> UserHasDayRecord(Guid id, string userId)
	{
		return _context.DayRecords
			.AnyAsync(dr => dr.Id == id && dr.UserId == userId);
	}
}
