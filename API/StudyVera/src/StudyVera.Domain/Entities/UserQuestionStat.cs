using System;
using System.Collections.Generic;
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

    public int SolvedCount { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount { get; set; }

    public DateTime LastAttemptAt { get; set; } = DateTime.UtcNow;
}