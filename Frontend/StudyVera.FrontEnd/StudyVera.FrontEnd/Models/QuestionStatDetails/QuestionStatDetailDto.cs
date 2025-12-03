namespace StudyVera.FrontEnd.Models.QuestionStatDetails;

public class QuestionStatDetailDto
{
    public int Id { get; set; }
    public int SolvedCount { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount => SolvedCount - CorrectCount;

    public DateTime AttemptedAt { get; set; }
}
