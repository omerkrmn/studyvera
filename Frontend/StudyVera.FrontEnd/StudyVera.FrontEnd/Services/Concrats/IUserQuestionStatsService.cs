using StudyVera.FrontEnd.Models.UserQuestionStat;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IUserQuestionStatsService
{
    Task<List<UserQuestionStatDto>> GetAll();
    Task Add(AddUserQuestionStatDto request);
}
