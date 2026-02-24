using System;

namespace StudyVera.Domain.Entities;

public class StudySession
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    
    public int? LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    public int? TopicId { get; set; }
    public Topic? Topic { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int DurationMinutes { get; set; }
    
    public string? Note { get; set; }
    public bool IsCompleted { get; set; }
}