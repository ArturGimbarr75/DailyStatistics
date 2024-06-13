using DailyStatistics.Model;

namespace DailyStatistics.Persistence.Repositories;

public interface ITrackingActivityRecordRepository
{
	Task<TrackingActivityRecord?> GetTrackingActivityRecordAsync(Guid id);
	Task<IEnumerable<TrackingActivityRecord>> GetTrackingActivityRecordsFromDayAsync(DateOnly date, string userId);
	Task<TrackingActivityRecord?> AddTrackingActivityRecordAsync(TrackingActivityRecord trackingActivityRecord);
	Task<TrackingActivityRecord?> UpdateTrackingActivityRecordAsync(TrackingActivityRecord trackingActivityRecord);
	Task<bool> DeleteTrackingActivityRecordAsync(Guid id);
	Task<bool> DayHasRecordWithKind(DateOnly date, string userId, Guid kindId);
	Task<bool> UserOwnsRecord(string userId, Guid recordId);
}
