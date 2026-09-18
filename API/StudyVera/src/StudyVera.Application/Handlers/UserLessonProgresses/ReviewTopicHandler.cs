using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserLessonProgresses;

public class ReviewTopicHandler : IRequestHandler<ReviewTopicCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public ReviewTopicHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(ReviewTopicCommand request, CancellationToken cancellationToken)
    {
        var ulp = await _manager.UserLessonProgressRepository
            .FindByCondition(u => u.Id == request.ulpId && u.UserId == request.UserId, trackChanges: true)
            .FirstOrDefaultAsync(cancellationToken);

        if (ulp == null)
            throw new NotFoundException($"UserLessonProgress not found.");

        ulp.LastUpdated= DateTime.UtcNow;
        if (request.DurationMinutes.HasValue)
        {
            ulp.DurationMinutes = (ulp.DurationMinutes ?? 0) + request.DurationMinutes.Value;
            var weekStart = UserWeeklyGoal.GetCurrentWeekStartDate();
            var weeklyGoal = await _manager.UserWeeklyGoalRepository.GetCurrentGoalAsync(request.UserId, weekStart, cancellationToken);
            if (weeklyGoal == null)
            {
                var profile = await _manager.UserProfileRepository
                    .FindByCondition(us => us.UserId == request.UserId, false)
                    .FirstOrDefaultAsync(cancellationToken);

                _manager.UserWeeklyGoalRepository.Create(new UserWeeklyGoal
                {
                    UserId = request.UserId,
                    WeekStartDate = weekStart,
                    TargetQuestionCount = profile?.WeeklyQuestionGoal ?? 500,
                    TargetStudyMinutes = (profile?.DailyStudyMinuteGoal ?? 60) * 7,
                    CurrentStudyMinutes = request.DurationMinutes.Value
                });
            }
            else
            {
                weeklyGoal.CurrentStudyMinutes += request.DurationMinutes.Value;
            }
        }

        _manager.UserActivityHistoryRepository.Create(new()
        {
            UserId = request.UserId,
            ActivityType = ActivityType.TopicReviewed,
            TopicId = ulp.TopicId,
            Description = $"User reviewed topic ID {ulp.TopicId}."
        });

        await _manager.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}