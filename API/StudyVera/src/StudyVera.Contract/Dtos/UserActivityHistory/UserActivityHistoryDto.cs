using StudyVera.Domain.Enums;

namespace StudyVera.Contract.Dtos.UserActivityHistory;

public class UserActivityHistoryDto
{
    public int Id { get; set; }
    public DateTime ActivityDate { get; set; } = DateTime.UtcNow;
    public ActivityType? ActivityType { get; set; }
    public string Description { get; set; } = string.Empty;
}
