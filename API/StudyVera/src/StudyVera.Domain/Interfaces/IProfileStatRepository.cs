using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IProfileStatRepository : IRepository<ProfileStat>
{
    Task<int> GetScoreByUserAsync(Guid userId, CancellationToken ct);
    Task<int> GetGlobalRankAsync(Guid userId, CancellationToken ct);
    Task UpdateScoreAsync(Guid userId, CancellationToken ct);
    Task UpdateScoreByCorrectQuestionCountAsync(Guid userId, int solvedQuestionCount, CancellationToken ct, int priority = 5);

    Task<ProfileStat?> GetByUserAsync(Guid userId, CancellationToken ct);
}
