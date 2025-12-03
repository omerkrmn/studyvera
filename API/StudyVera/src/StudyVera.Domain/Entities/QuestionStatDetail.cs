using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class QuestionStatDetail
{
    public int Id { get; set; }

    public int UserQuestionStatId { get; set; }
    public UserQuestionStat userQuestionStat { get; set; } = null!;

    public int SolvedCount { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount => SolvedCount - CorrectCount;

    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
}
