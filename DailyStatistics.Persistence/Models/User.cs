using Microsoft.AspNetCore.Identity;

namespace DailyStatistics.Persistence.Models;

public sealed class User : IdentityUser
{
	public string Name { get; set; } = default!;
	public string Surname { get; set; } = default!;
	public Guid? SelectedProfileImageId { get; set; }
	public ProfileImage? SelectedProfileImage { get; set; } = default!;
	public ICollection<DayRecord> DayRecords { get; set; } = new List<DayRecord>();
	public ICollection<ProfileImage> ProfileImages { get; set; } = new List<ProfileImage>();
	public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
	public ICollection<TrackingActivityGroup> TrackingActivityGroups { get; set; } = new List<TrackingActivityGroup>();
	public ICollection<TrackingActivityKind> TrackingActivityKinds { get; set; } = new List<TrackingActivityKind>();
}
