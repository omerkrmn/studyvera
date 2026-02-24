using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities.Mock;

public class UserMockExam
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public int ExamId { get; set; }
    public Exam Exam { get; set; } = null!;

    public string? Publisher { get; set; } 
    public string? ExamName { get; set; }

    public DateTime ExamDate { get; set; } = DateTime.UtcNow;

    public double TotalNet { get; set; }
    public int TotalCorrect { get; set; }
    public int TotalWrong { get; set; }
    public int TotalEmpty { get; set; }

    public ICollection<UserMockExamDetail> Details { get; set; } = new List<UserMockExamDetail>();
}
