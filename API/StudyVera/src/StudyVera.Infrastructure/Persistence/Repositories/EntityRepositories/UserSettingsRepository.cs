using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserSettingsRepository : RepositoryBase<UserSettings>, IUserSettingsRepository
{
    public UserSettingsRepository(AppDbContext context) : base(context)
    {
    }
    public Task<UserSettings> GetByUserIdAsync(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
