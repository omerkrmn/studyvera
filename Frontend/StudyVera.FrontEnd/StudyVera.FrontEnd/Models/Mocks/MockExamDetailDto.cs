using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.FrontEnd.Models.Mocks;


public class MockExamDetailDto
{
    public int LessonId { get; set; }

    [Range(0, 100, ErrorMessage = "Doğru sayısı 0-100 arasında olmalıdır.")]
    public int CorrectCount { get; set; }

    [Range(0, 100, ErrorMessage = "Yanlış sayısı 0-100 arasında olmalıdır.")]
    public int WrongCount { get; set; }
}