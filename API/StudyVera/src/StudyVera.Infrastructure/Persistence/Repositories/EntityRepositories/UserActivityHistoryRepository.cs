using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserActivityHistoryRepository : RepositoryBase<UserActivityHistory>, IUserActivityHistoryRepository
{
    public UserActivityHistoryRepository(AppDbContext context) : base(context)
    {
    }

    public Task AddAsync(Guid userId, string activityType, string description, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserActivityHistory>> GetAllByUser(Guid guid, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserActivityHistory>> GetAllByUserAndType(Guid guid, ActivityType activityType, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<List<DateTime>> GetByActivityTimeOfAllTime(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
