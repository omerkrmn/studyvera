using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;

namespace StudyVera.Domain.Interfaces;

public interface IUserActivityHistoryRepository : IRepository<UserActivityHistory>
{ 
    public Task<List<UserActivityHistory>> GetAllByUserAsycn(Guid guid, CancellationToken ct);
    public Task<List<UserActivityHistory>> GetAllByUserAndType(Guid guid, ActivityType activityType, CancellationToken ct);
    public Task<List<DateTime>> GetByActivityTimeOfAllTime(Guid userId, CancellationToken ct);
}
