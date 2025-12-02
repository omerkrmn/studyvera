using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserActivityHistoryRepository : RepositoryBase<UserActivityHistory>, IUserActivityHistoryRepository
{
    public UserActivityHistoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<UserActivityHistory>> GetAllByUserAsycn(Guid guid, CancellationToken ct)
    {
        return await FindByCondition(u => u.UserId == guid, false).OrderByDescending(s => s.ActivityDate).ToListAsync(ct);
    }

    public async Task<List<UserActivityHistory>> GetAllByUserAndType(Guid guid, ActivityType activityType, CancellationToken ct)
    {
        return await FindByCondition(u => u.UserId == guid && u.ActivityType == activityType, false).ToListAsync(ct);
    }

    public async Task<List<DateTime>> GetByActivityTimeOfAllTime(Guid userId, CancellationToken ct)
    {
        var allActivities = await FindByCondition(u => u.UserId == userId, false)
                                 .Select(a=>a.ActivityDate)
                                 .OrderDescending()
                                 .ToListAsync(ct);

        return allActivities;
    }
}
