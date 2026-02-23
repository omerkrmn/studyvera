using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.FrontEnd.Models.Mocks;

public class UserMockExamDetailResponse
{
    public int Id { get; set; }
    public string? ExamName { get; set; }
    public double TotalNet { get; set; }
    public List<MockDetailItemResponse> Details { get; set; } = new();
}