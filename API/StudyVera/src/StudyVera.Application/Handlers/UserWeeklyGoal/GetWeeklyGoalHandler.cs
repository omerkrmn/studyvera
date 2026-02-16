using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos.UserWeeklyGoals;
using StudyVera.Application.Features.UserWeeklyGoal.Queries;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserWeeklyGoals;

public class GetWeeklyGoalHandler : IRequestHandler<GetWeeklyGoalQuery, UserWeeklyGoalDto>
{
    private readonly IRepositoryManager _manager;

    public GetWeeklyGoalHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<UserWeeklyGoalDto> Handle(GetWeeklyGoalQuery request, CancellationToken cancellationToken)
    {
        var weekStart = UserWeeklyGoal.GetCurrentWeekStartDate();
        var goal = await _manager.UserWeeklyGoalRepository.GetCurrentGoalAsync(request.UserId, weekStart, cancellationToken);

        if (goal == null)
        {
            var profile = await _manager.UserProfileRepository
                .FindByCondition(x => x.UserId == request.UserId, false)
                .FirstOrDefaultAsync(cancellationToken);

            goal = new UserWeeklyGoal
            {
                UserId = request.UserId,
                WeekStartDate = weekStart,
                TargetQuestionCount = profile?.WeeklyQuestionGoal ?? 500,
                TargetStudyMinutes = (profile?.DailyStudyMinuteGoal ?? 60) * 7,
                CurrentQuestionCount = 0,
                CurrentStudyMinutes = 0
            };

            _manager.UserWeeklyGoalRepository.Create(goal);
            await _manager.SaveChangesAsync();
        }

        return goal.Adapt<UserWeeklyGoalDto>();
    }
}
