using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class ProfileStatRepository : RepositoryBase<ProfileStat>, IProfileStatRepository
{
    public ProfileStatRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> GetScoreByUser(Guid userId, CancellationToken ct)
    {
        var userscore = await FindByCondition(u => u.UserId == userId, false).FirstOrDefaultAsync(ct);
        return userscore?.Score ?? 0;
    }

    public async Task UpdateScore(Guid userId, CancellationToken ct)
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
