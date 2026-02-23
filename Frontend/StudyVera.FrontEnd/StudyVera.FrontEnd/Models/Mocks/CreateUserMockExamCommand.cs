using System.Text.Json.Serialization;

namespace StudyVera.FrontEnd.Models.Mocks;

public class CreateUserMockExamCommand
{

    public int ExamId { get; set; }
    public string? Publisher { get; set; }
    public string? ExamName { get; set; }
    public DateTime ExamDate { get; set; }
    public List<MockExamDetailDto> Details { get; set; } = new List<MockExamDetailDto>();
}