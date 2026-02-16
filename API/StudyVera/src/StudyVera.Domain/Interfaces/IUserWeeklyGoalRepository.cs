using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IUserWeeklyGoalRepository : IRepository<UserWeeklyGoal>
{
    public Task<UserWeeklyGoal> GetCurrentGoalAsync(Guid userId, DateTime weekStart,CancellationToken ct);
}
