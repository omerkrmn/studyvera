using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IUserProfileRepository : IRepository<UserProfile>
{
    Task<UserProfile> GetByUserIdAsync(Guid userId, CancellationToken ct);
}
