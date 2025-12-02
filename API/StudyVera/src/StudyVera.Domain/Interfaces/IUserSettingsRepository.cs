using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IUserSettingsRepository : IRepository<UserSettings>
{
    Task<UserSettings> GetByUserIdAsync(Guid userId, CancellationToken ct);
    // update zaten var;
}
