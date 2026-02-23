using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.FrontEnd.Models.Mocks;

public class UserMockExamResponse
{
    public int Id { get; set; }
    public string? ExamName { get; set; }
    public string? Publisher { get; set; }
    public DateTime ExamDate { get; set; }
    public double TotalNet { get; set; }
    public int TotalCorrect { get; set; }
    public int TotalWrong { get; set; }
}