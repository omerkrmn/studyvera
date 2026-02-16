using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserWeeklyGoalRepository : RepositoryBase<UserWeeklyGoal>, IUserWeeklyGoalRepository
{
    public UserWeeklyGoalRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserWeeklyGoal> GetCurrentGoalAsync(Guid userId, DateTime weekStart,CancellationToken ct)
    {
        return await _context.UserWeeklyGoals
                .FirstOrDefaultAsync(g => g.UserId == userId && g.WeekStartDate == weekStart,ct);
    }
}
