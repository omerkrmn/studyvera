using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities.Mock;

public class UserMockExamDetail
{
    public int Id { get; set; }
    public int UserMockExamId { get; set; }
    public UserMockExam UserMockExam { get; set; } = null!;

    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public int CorrectCount { get; set; }
    public int WrongCount { get; set; }

    public int EmptyCount { get; set; }
    public double Net => CorrectCount - (WrongCount * 0.25d);
}