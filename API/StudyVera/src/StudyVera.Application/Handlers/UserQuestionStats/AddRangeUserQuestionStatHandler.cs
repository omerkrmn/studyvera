using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserQuestionStats.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserQuestionStats;

public class AddRangeUserQuestionStatHandler : IRequestHandler<AddRangeUserQuestionStatCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public AddRangeUserQuestionStatHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(AddRangeUserQuestionStatCommand request, CancellationToken ct)
    {
        if (request.Items == null || !request.Items.Any())
            return Unit.Value;

        var now = DateTime.UtcNow;
        var today = now.Date;
        var weekStart = UserWeeklyGoal.GetCurrentWeekStartDate();

        // Fetch User level data once
        var weeklyGoal = await _manager.UserWeeklyGoalRepository.GetCurrentGoalAsync(request.UserId, weekStart, ct);
        if (weeklyGoal == null)
        {
            var profile = await _manager.UserProfileRepository
                .FindByCondition(us => us.UserId == request.UserId, false)
                .FirstOrDefaultAsync(ct);

            weeklyGoal = new UserWeeklyGoal
            {
                UserId = request.UserId,
                WeekStartDate = weekStart,
                TargetQuestionCount = profile?.WeeklyQuestionGoal ?? 500,
                TargetStudyMinutes = (profile?.DailyStudyMinuteGoal ?? 60) * 7,
                CurrentQuestionCount = 0,
                CurrentStudyMinutes = 0
            };
            _manager.UserWeeklyGoalRepository.Create(weeklyGoal);
        }

        var profileStat = await _manager.ProfileStatRepository.GetByUserAsync(request.UserId, ct);
        bool streakUpdated = false;

        foreach (var item in request.Items)
        {
            var topic = await _manager.TopicRepository
                .FindByCondition(t => t.Id == item.TopicId, false)
                .SingleOrDefaultAsync(ct);

            if (topic == null)
                continue; // Skip invalid topics

            var uqs = await _manager.UserQuestionStatRepository.FindByCondition(
                u => u.UserId == request.UserId && u.TopicId == item.TopicId, true)
                .FirstOrDefaultAsync(ct);

            int topicPriority = topic?.Priority ?? 3;

            // Update Weekly Goal
            weeklyGoal.CurrentQuestionCount += item.SolvedCount;
            weeklyGoal.CurrentStudyMinutes += item.DurationMinutes ?? 0;

            // Update UQS and add Detail
            var newDetail = new QuestionStatDetail
            {
                CorrectCount = item.CorrectCount,
                SolvedCount = item.SolvedCount,
                AttemptedAt = now,
                DurationMinutes = item.DurationMinutes
            };

            if (uqs != null)
            {
                uqs.TotalSolvedCount += item.SolvedCount;
                uqs.TotalCorrectCount += item.CorrectCount;
                uqs.TotalTimeSpentInMinutes += item.DurationMinutes ?? 0;
                uqs.LastAttemptAt = now;
                newDetail.UserQuestionStatId = uqs.Id;
                _manager.QuestionStatDetailRepository.Create(newDetail);
            }
            else
            {
                _manager.UserQuestionStatRepository.Create(new UserQuestionStat
                {
                    UserId = request.UserId,
                    TopicId = item.TopicId,
                    TotalCorrectCount = item.CorrectCount,
                    TotalSolvedCount = item.SolvedCount,
                    TotalTimeSpentInMinutes = item.DurationMinutes ?? 0,
                    LastAttemptAt = now,
                    QuestionStatDetails = new List<QuestionStatDetail> { newDetail }
                });
            }

            // Update ProfileStat
            if (profileStat != null)
            {
                profileStat.Score += (item.CorrectCount * topicPriority);

                if (!streakUpdated)
                {
                    var lastDate = profileStat.LastActivityDate?.Date ?? DateTime.MinValue.Date;

                    if (lastDate != today)
                    {
                        profileStat.CurrentStreak = (lastDate == today.AddDays(-1)) ? profileStat.CurrentStreak + 1 : 1;

                        if (profileStat.CurrentStreak > profileStat.BestStreak)
                            profileStat.BestStreak = profileStat.CurrentStreak;
                    }
                    streakUpdated = true;
                }

                profileStat.LastActivityDate = now;
            }

            // Create Activity History
            _manager.UserActivityHistoryRepository.Create(new UserActivityHistory
            {
                UserId = request.UserId,
                ActivityType = ActivityType.SolvedAQuestion,
                TopicId = item.TopicId,
                LessonId = topic?.LessonId,
                Description = $"{item.SolvedCount} adet {topic?.Name} sorusu çözüldü.",
                ActivityDate = now
            });
        }

        await _manager.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
