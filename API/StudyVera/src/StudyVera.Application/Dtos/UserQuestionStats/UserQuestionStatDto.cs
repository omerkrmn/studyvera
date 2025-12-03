using StudyVera.Application.Dtos.QuestionStatDetails;
using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.UserQuestionStats;

public class UserQuestionStatDto
{
    public int Id { get; set; }


    public int TopicId { get; set; }
    public TopicDto Topic { get; set; } = null!;

    public int TotalSolvedCount { get; set; }
    public int TotalCorrectCount { get; set; }
    public int TotalWrongCount => TotalSolvedCount - TotalCorrectCount;

    public float AccuracyRate => TotalSolvedCount == 0 ? 0 : (float)TotalCorrectCount / (float)TotalSolvedCount;

    public List<QuestionStatDetailDto> QuestionStatDetail { get; set; }


    public DateTime LastAttemptAt { get; set; }

}
