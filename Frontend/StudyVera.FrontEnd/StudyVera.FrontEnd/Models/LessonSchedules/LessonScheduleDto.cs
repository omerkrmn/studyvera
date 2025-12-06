namespace StudyVera.FrontEnd.Models.LessonSchedules;

public class LessonScheduleDto
{
    public int LessonId { get; set; }
    public int TopicId { get; set; }
    public int DayOfWeek { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}
