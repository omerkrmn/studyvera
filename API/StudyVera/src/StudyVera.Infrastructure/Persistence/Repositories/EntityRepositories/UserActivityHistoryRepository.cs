using Microsoft.EntityFrameworkCore;
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

    public async Task AddAsync(Guid userId, ActivityType activityType, string description, CancellationToken ct)
    {

        Create(new UserActivityHistory()
                        {
                            UserId = userId,
                            ActivityDate = DateTime.Now,
                            Description = description,
                            ActivityType = activityType
                        }
        );

    }

    public async Task<List<UserActivityHistory>> GetAllByUser(Guid guid, CancellationToken ct)
    {
        return await FindByCondition(u=>u.UserId == guid,false).ToListAsync(ct);
    }

    public async Task<List<UserActivityHistory>> GetAllByUserAndType(Guid guid, ActivityType activityType, CancellationToken ct)
    {
        return await FindByCondition(u => u.UserId == guid && u.ActivityType ==activityType, false).ToListAsync(ct);
    }

    public async Task<List<DateTime>> GetByActivityTimeOfAllTime(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
