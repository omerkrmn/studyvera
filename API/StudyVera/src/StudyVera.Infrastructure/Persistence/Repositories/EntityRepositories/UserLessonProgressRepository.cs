using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserLessonProgressRepository : RepositoryBase<UserLessonProgress>, IUserLessonProgressRepository
{
    public UserLessonProgressRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<UserLessonProgress>> GetAllByProgressStatusAndLastUpdatedBeforeAsync(Guid userId, int progressStatus, DateTime lastUpdated, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserLessonProgress>> GetAllByProgressStatusAsync(Guid userId, int progressStatus, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
