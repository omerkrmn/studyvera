using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class Lesson
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;


    public ICollection<Topic> Topics { get; set; } = [];

}
