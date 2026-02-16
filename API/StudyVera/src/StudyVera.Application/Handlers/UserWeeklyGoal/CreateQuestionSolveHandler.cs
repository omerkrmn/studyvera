using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Features.UserWeeklyGoal.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers;

public class CreateQuestionSolveHandler : IRequestHandler<CreateQuestionSolveCommand, Unit>
{
    private readonly IRepositoryManager _repository;

    public CreateQuestionSolveHandler(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateQuestionSolveCommand request, CancellationToken cancellationToken)
    {
        var weekStart = GetFirstDayOfWeek(DateTime.UtcNow);

        var weeklyGoal = await _repository.UserWeeklyGoalRepository.GetCurrentGoalAsync(request.UserId, weekStart, cancellationToken);

        if (weeklyGoal == null)
        {
            var userProfile = await _repository.UserProfileRepository
                .FindByCondition(us => us.UserId == request.UserId, false)
                .SingleOrDefaultAsync(cancellationToken);

            weeklyGoal = new UserWeeklyGoal
            {
                UserId = request.UserId,
                WeekStartDate = weekStart,
                TargetQuestionCount = userProfile.WeeklyQuestionGoal,
                TargetStudyMinutes = userProfile.DailyStudyMinuteGoal * 7,
                CurrentQuestionCount = request.SolvedCount,
                CurrentStudyMinutes = request.StudyMinutes
            };
            _repository.UserWeeklyGoalRepository.Create(weeklyGoal);
        }
        else
        {
            weeklyGoal.CurrentQuestionCount += request.SolvedCount;
            weeklyGoal.CurrentStudyMinutes += request.StudyMinutes;
            _repository.UserWeeklyGoalRepository.Update(weeklyGoal);
        }

        await _repository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private DateTime GetFirstDayOfWeek(DateTime date)
    {
        int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(-1 * diff).Date;
    }
}
