using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;

namespace StudyVera.Domain.Interfaces;

public interface ITopicRepository : IRepository<Topic>
{
    public Task<List<Topic>> GetAllByTargetAsync(TargetExam exam, CancellationToken ct = default);
    public Task<List<Topic>> GetAllByLessonIdAsync(TargetExam exam, int lessonId, CancellationToken ct = default);
}
