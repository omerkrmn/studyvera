

using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;

namespace StudyVera.Domain.Entities;

public class UserLessonProgress
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;
    
    public ProgressStatus ProgressStatus { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}
