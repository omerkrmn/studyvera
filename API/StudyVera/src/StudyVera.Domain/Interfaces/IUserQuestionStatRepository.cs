using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IUserQuestionStatRepository : IRepository<UserQuestionStat>
{
    public Task<(int TotalSolvedCount, int TotalCorrectCount)> GetSumAsync(Guid userId, CancellationToken c);
}
