using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IProfileStatRepository : IRepository<ProfileStat>
{
    public Task<int> GetScoreByUser(Guid userId, CancellationToken ct);
    public Task UpdateScore(Guid userId, CancellationToken ct);
    public Task<int> GetGlobalRankAsync(Guid userId, CancellationToken ct);

}
