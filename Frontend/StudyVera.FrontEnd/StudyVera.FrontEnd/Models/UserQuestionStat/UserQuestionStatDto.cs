using StudyVera.FrontEnd.Models.Topics;

namespace StudyVera.FrontEnd.Models.UserQuestionStat;

public class UserQuestionStatDto
{
    public int Id { get; set; }


    public int TopicId { get; set; }
    public TopicDto Topic { get; set; } = null!;

    public int SolvedCount { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount { get; set; }

    public float AccuracyRate { get; set; }

    public DateTime LastAttemptAt { get; set; } = DateTime.UtcNow;
}
