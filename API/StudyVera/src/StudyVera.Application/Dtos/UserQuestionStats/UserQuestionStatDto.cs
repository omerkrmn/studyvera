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

    public int SolvedCount { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount { get; set; }

    public float AccuracyRate { get; set; }

    public DateTime LastAttemptAt { get; set; } = DateTime.UtcNow;

}
