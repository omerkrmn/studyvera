namespace StudyVera.FrontEnd.Models.Topics;

public class TopicDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LessonId { get; set; }
    public byte Priority { get; set; }
}
