using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class LessonSchedule
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; }

    public int LessonId { get; set; }
    public Lesson Lesson { get; set; }

    public int? TopicId { get; set; }
    public Topic? Topic { get; set; }

    public int DayOfWeek { get; set; }
    
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; }= DateTime.UtcNow;

}
