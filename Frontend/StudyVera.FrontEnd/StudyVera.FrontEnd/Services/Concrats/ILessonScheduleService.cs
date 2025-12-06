using StudyVera.FrontEnd.Models.LessonSchedules;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface ILessonScheduleService 
{
    public Task<List<LessonScheduleDto>> GetAll();
    public Task Add(AddLessonScheduleDto dto);
}
