using StudyVera.FrontEnd.Models.Lessons;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface ILessonService
{
    public Task<List<LessonDto>> GetAll();
}
