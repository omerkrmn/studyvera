using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Models;
using StudyVera.Application.Dtos;
using StudyVera.Application.Dtos.UserQuestionStats;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserQuestionStatRepository : RepositoryBase<UserQuestionStat>, IUserQuestionStatRepository
{
    public UserQuestionStatRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<UserQuestionStatDto>> GetAllByUser(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
    public async Task<(int TotalSolvedCount, int TotalCorrectCount)> GetSumAsync(Guid userId, CancellationToken c)
    {
        var query = FindByCondition(uqs => uqs.UserId == userId, false)
            .AsNoTracking();

        var totalSolved  = await query.SumAsync(uqs => uqs.TotalSolvedCount, c);
        var totalCorrect = await query.SumAsync(uqs => uqs.TotalCorrectCount, c);

        return (totalSolved, totalCorrect);
    }

}
