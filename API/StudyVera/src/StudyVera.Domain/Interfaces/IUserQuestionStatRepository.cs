using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IUserQuestionStatRepository : IRepository<UserQuestionStat>
{
    //public Task<List<UserQuestionStatDto>> GetAllByUser(Guid userId, UserQuestionStatParameters uqsParameters, CancellationToken ct);
}
