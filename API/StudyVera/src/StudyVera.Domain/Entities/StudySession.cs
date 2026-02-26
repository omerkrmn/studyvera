using System;
using StudyVera.Domain.Entities.Identity;

namespace StudyVera.Domain.Entities;

public class StudySession
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;
    
    public int? LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public int? TopicId { get; set; }
    public Topic? Topic { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int DurationMinutes { get; set; }
    
    public string? Note { get; set; }
    public bool IsCompleted { get; set; }

    public void CompleteSession()
    {
        if (IsCompleted) return;

        EndTime = DateTime.UtcNow;
        IsCompleted = true;
        // Sürenin negatif çıkmaması için kontrol ve dakika cinsinden hesaplama
        DurationMinutes = (int)Math.Max(0, (EndTime - StartTime).TotalMinutes);
    }
}