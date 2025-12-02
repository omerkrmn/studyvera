using StudyVera.FrontEnd.Models.UserLessonProgress;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IUserLessonProgressService
{
    Task<List<UserLessonProgressDto>> GetAll();
    Task Add(AddUserLessonProgressDto dto);
    Task AddRange();
    Task Update(int ulpId);
}
