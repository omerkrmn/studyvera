namespace StudyVera.FrontEnd.Models.LessonSchedules;

public class AddLessonScheduleDto
{
    public int LessonId { get; set; }
    public int TopicId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
}

