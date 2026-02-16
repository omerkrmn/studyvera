using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserLessonProgressRepository : RepositoryBase<UserLessonProgress>, IUserLessonProgressRepository
{
    public UserLessonProgressRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(Guid userId, int topicId, CancellationToken ct) =>
         await FindByCondition(ulp => ulp.UserId == userId && ulp.TopicId == topicId, false).AnyAsync(ct);

    public async Task<List<UserLessonProgress>> GetAllByProgressStatusAndLastUpdatedBeforeAsync(Guid userId, ProgressStatus progressStatus, DateTime lastUpdated, bool trackChanges, CancellationToken ct) =>
        await FindByCondition(ulp => ulp.UserId == userId && ulp.ProgressStatus == progressStatus, trackChanges).Where(ulp => ulp.LastUpdated < lastUpdated).ToListAsync(ct);

    public async Task<List<UserLessonProgress>> GetAllByProgressStatusAsync(Guid userId, ProgressStatus progressStatus, bool trackChanges, CancellationToken ct) =>
        await FindByCondition(ulp => ulp.UserId == userId && ulp.ProgressStatus == progressStatus, trackChanges).ToListAsync(ct);
}
