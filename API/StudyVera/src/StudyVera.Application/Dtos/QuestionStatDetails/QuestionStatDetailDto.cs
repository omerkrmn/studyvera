using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.QuestionStatDetails;

public class QuestionStatDetailDto
{
    public int SolvedCount { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount => SolvedCount - CorrectCount;

    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
}
