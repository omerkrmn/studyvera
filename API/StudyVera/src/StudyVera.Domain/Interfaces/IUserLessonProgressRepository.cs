using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;

namespace StudyVera.Domain.Interfaces;

public interface IUserLessonProgressRepository : IRepository<UserLessonProgress>
{
    public Task<List<UserLessonProgress>> GetAllByProgressStatusAsync(Guid userId, ProgressStatus progressStatus, bool trackChanges, CancellationToken ct);
    public Task<List<UserLessonProgress>> GetAllByProgressStatusAndLastUpdatedBeforeAsync(Guid userId, ProgressStatus progressStatus, DateTime lastUpdated, bool trackChanges, CancellationToken ct);
    public Task<bool> ExistsAsync(Guid userId, int topicId, CancellationToken ct);
}
