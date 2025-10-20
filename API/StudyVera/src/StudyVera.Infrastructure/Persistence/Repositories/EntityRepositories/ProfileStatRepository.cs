using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class ProfileStatRepository : RepositoryBase<ProfileStat>, IProfileStatRepository
{
    public ProfileStatRepository(AppDbContext context) : base(context)
    {
    }

    public Task<int> GetScoreByUser(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public void UpdateScore(Guid userId, ProgressStatus progressStatus, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
