using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class ProfileStatRepository : RepositoryBase<ProfileStat>, IProfileStatRepository
{
    public ProfileStatRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> GetGlobalRankAsync(Guid userId, CancellationToken ct)
    {
        string sql = @"
        SELECT 
            t.UserRank
        FROM  
            (
                SELECT 
                    UserId,
                    CAST(ROW_NUMBER() OVER (ORDER BY Score DESC) AS INT) AS UserRank
                FROM 
                    ProfileStats
            ) AS t
        WHERE 
            t.UserId = @p0
    ";

        var userIdParameter = new SqlParameter("@p0", userId);

        var result = await _context.RankResults
                                   .FromSqlRaw(sql, userIdParameter)
                                   .AsNoTracking()
                                   .Select(r => r.UserRank)
                                   .FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<int> GetScoreByUserAsync(Guid userId, CancellationToken ct)
    {
        var userscore = await FindByCondition(u => u.UserId == userId, false).FirstOrDefaultAsync(ct);
        return userscore?.Score ?? 0;
    }

    public async Task UpdateScoreByCorrectQuestionCountAsync(Guid userId, int solvedQuestinCount, CancellationToken ct,int priority = 5)
    {
        var userScore = FindByCondition(u => u.UserId == userId, true).FirstOrDefault();

        if (userScore != null)
        {
            userScore.Score += solvedQuestinCount * priority;
            Update(userScore);
        }
        else
        {
            Create(new ProfileStat { UserId = userId, Score = solvedQuestinCount * 10 });
        }
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateScoreAsync(Guid userId, CancellationToken ct)
    {
        var userScore = FindByCondition(u => u.UserId == userId, true).FirstOrDefault();

        if (userScore != null)
        {
            userScore.Score += 10;
            Update(userScore);
        }
        Create(new ProfileStat { UserId = userId, Score = 10 });
        await _context.SaveChangesAsync(ct);
    }
}
