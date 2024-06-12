using DailyStatistics.Persistence.Models;

namespace DailyStatistics.Persistence.Repositories;

public interface ITrackingActivityKindRepository
{
	Task<TrackingActivityKind?> AddAsync(TrackingActivityKind trackingActivityKind);
	Task<TrackingActivityKind?> GetAsync(Guid id);
	Task<TrackingActivityKind?> UpdateAsync(TrackingActivityKind trackingActivityKind);
	Task<bool> DeleteAsync(Guid id);
	Task<IEnumerable<TrackingActivityKind>> GetAllOfUserAsync(string userId);
	Task<bool> UserOwnsTrackingActivityKind(string userId, Guid trackingActivityKindId);
	Task<bool> ExistsWithNameAsync(string userId, string name);
	Task<bool> ExistsWithNameButNotWithIdAsync(string userId, string name, Guid id);
}
