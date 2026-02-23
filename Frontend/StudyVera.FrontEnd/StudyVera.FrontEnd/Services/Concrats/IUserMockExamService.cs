using StudyVera.FrontEnd.Models.Mocks;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IUserMockExamService
{
    Task<List<UserMockExamResponse>> GetUserMockExamsAsync();

    Task<UserMockExamDetailResponse?> GetMockExamDetailAsync(int id);

    Task<int> CreateMockExamAsync(CreateUserMockExamCommand request);
}