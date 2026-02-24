using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.Mocks;

public class MockExamDetailDto
{
    public int LessonId { get; set; }
    public int CorrectCount { get; set; }
    public int WrongCount { get; set; }
}