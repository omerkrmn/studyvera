using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IUserActivityHistoryRepository
{
    public Task AddAsync(Guid userId, string activityType, string description, CancellationToken ct);
    public Task<List<UserActivityHistory>> GetAllByUser(Guid guid, CancellationToken ct);
    public Task<List<UserActivityHistory>> GetAllByUserAndType(Guid guid, ActivityType activityType, CancellationToken ct);
    public Task<List<DateTime>> GetByActivityTimeOfAllTime(Guid userId, CancellationToken ct);
}
