using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Range(1, 5)]
    public byte Priority { get; set; } // 1-5 
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
}