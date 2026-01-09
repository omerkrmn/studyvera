using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(AppDbContext context) : base(context)
    {
    }
    public Task<UserProfile> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
