using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class Topic
{
    public int Id { get; set; }
    // Sayılar, Cebir, Geometri, Fonksiyonlar, Trigonometri, Analitik Geometri,Dünya tarihi
    public string Name { get; set; } = string.Empty;


    // Relationships
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
}