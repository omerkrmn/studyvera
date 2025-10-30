using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;


    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
}