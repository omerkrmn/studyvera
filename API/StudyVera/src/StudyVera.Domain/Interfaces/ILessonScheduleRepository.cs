using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface ILessonScheduleRepository
{
    Task<IEnumerable<LessonSchedule>> GetWeeklyScheduleAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<LessonSchedule?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Create(LessonSchedule schedule);
    void Delete(LessonSchedule schedule);
}
