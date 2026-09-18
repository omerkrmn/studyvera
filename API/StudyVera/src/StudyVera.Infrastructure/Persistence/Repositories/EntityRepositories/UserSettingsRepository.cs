using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
{
    public UserProfileRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<UserProfile> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        return await FindByCondition(up => up.UserId == userId, false)
            .FirstOrDefaultAsync(ct);
    }
}
