using MediatR;
using StudyVera.Application.Dtos.Mocks;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.Mock.UserMockExams.Commands;

public class CreateUserMockExamCommand : IRequest<int>
{

    [JsonIgnore]
    public Guid UserId { get; set; }
    public int ExamId { get; set; }
    public string? Publisher { get; set; }
    public string? ExamName { get; set; }
    public DateTime ExamDate { get; set; }
    public List<MockExamDetailDto> Details { get; set; } = new();
}
