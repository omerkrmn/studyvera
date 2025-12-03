using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class UserQuestionStat
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public int TopicId { get; set; }
    public Topic Topic { get; set; } = null!;

    public int TotalSolvedCount { get; set; }
    public int TotalCorrectCount { get; set; }

    public int TotalWrongCount => TotalSolvedCount - TotalCorrectCount;

    public float AccuracyRate => TotalSolvedCount == 0 ? 0 : (float)TotalCorrectCount / (float)TotalSolvedCount;


    public DateTime LastAttemptAt { get; set; } = DateTime.UtcNow;
    public ICollection<QuestionStatDetail> QuestionStatDetails { get; set; } = new List<QuestionStatDetail>();
}