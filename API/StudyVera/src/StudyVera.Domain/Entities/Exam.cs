using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class Exam
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // Örn: YKS, KPSS, DGS
    public string Description { get; set; } = string.Empty;

    // Navigation
    public ICollection<Lesson> Lessons { get; set; } = [];
}
