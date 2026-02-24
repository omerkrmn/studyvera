using StudyVera.Domain.Entities.Mock;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserMockExamDetailRepository : RepositoryBase<UserMockExamDetail>, IUserMockExamDetailRepository
{
    public UserMockExamDetailRepository(AppDbContext context) : base(context)
    {
    }

}
