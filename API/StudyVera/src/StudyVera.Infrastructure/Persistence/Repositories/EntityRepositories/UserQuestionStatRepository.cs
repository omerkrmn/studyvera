using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserQuestionStatRepository : RepositoryBase<UserQuestionStat>, IUserQuestionStatRepository
{
    public UserQuestionStatRepository(AppDbContext context) : base(context)
    {
    }

    public Task<UserQuestionStat> GetAllByUser(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
