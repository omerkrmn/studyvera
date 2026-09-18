using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Features.StudySessions.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.StudySessions;

public class CreateStudySessionHandler : IRequestHandler<CreateStudySessionCommand, bool>
{
    private readonly IRepositoryManager _manager;

    public CreateStudySessionHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<bool> Handle(CreateStudySessionCommand request, CancellationToken cancellationToken)
    {
        var session = new StudySession
        {
            UserId = request.UserId,
            DurationMinutes = request.DurationMinutes,
            LessonId = request.LessonId,
            TopicId = request.TopicId,
            StartTime = DateTime.UtcNow.AddMinutes(-request.DurationMinutes),
            EndTime = DateTime.UtcNow,
            IsCompleted = true,
            Note = request.Note,
            SessionType = request.SessionType
        };
        
        _manager.StudySessionRepository.Create(session);

        var weekStart = UserWeeklyGoal.GetCurrentWeekStartDate();
        var weeklyGoal = await _manager.UserWeeklyGoalRepository.GetCurrentGoalAsync(request.UserId, weekStart, cancellationToken);

        if (weeklyGoal == null)
        {
            var userProfile = await _manager.UserProfileRepository
                .FindByCondition(us => us.UserId == request.UserId, false)
                .SingleOrDefaultAsync(cancellationToken);

            weeklyGoal = new UserWeeklyGoal
            {
                UserId = request.UserId,
                WeekStartDate = weekStart,
                TargetQuestionCount = userProfile?.WeeklyQuestionGoal ?? 0,
                TargetStudyMinutes = (userProfile?.DailyStudyMinuteGoal ?? 0) * 7,
                CurrentQuestionCount = 0,
                CurrentStudyMinutes = request.DurationMinutes
            };
            _manager.UserWeeklyGoalRepository.Create(weeklyGoal);
        }
        else
        {
            weeklyGoal.CurrentStudyMinutes += request.DurationMinutes;
            _manager.UserWeeklyGoalRepository.Update(weeklyGoal);
        }

        var activity = new UserActivityHistory
        {
            UserId = request.UserId,
            ActivityType = ActivityType.StudySessionCompleted,
            Description = $"{request.DurationMinutes} dakika ders çalışıldı.",
            ActivityDate = DateTime.UtcNow
        };
        _manager.UserActivityHistoryRepository.Create(activity);

        await _manager.SaveChangesAsync(cancellationToken);
        return true;
    }
}
