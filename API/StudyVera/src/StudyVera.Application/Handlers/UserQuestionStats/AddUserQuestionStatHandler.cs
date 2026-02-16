using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserQuestionStats.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserQuestionStats;

public class AddUserQuestionStatHandler : IRequestHandler<AddUserQuestionStatCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public AddUserQuestionStatHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(AddUserQuestionStatCommand request, CancellationToken ct)
    {
        var now = DateTime.UtcNow;
        var today = now.Date;
        var weekStart = UserWeeklyGoal.GetCurrentWeekStartDate();

        var uqs = await _manager.UserQuestionStatRepository.FindByCondition(
            uqs => uqs.UserId == request.UserId && uqs.TopicId == request.TopicId, true)
            .FirstOrDefaultAsync(ct);

        var topic = await _manager.TopicRepository
            .FindByCondition(t => t.Id == request.TopicId, false)
            .SingleOrDefaultAsync(ct);

        

        int topicPriority = topic?.Priority ?? 3;

        var weeklyGoal = await _manager.UserWeeklyGoalRepository.GetCurrentGoalAsync(request.UserId, weekStart, ct);
        if (weeklyGoal == null)
        {
            var profile = await _manager.UserProfileRepository
                .FindByCondition(us => us.UserId == request.UserId, false)
                .FirstOrDefaultAsync(ct);

            _manager.UserWeeklyGoalRepository.Create(new UserWeeklyGoal
            {
                UserId = request.UserId,
                WeekStartDate = weekStart,
                TargetQuestionCount = profile?.WeeklyQuestionGoal ?? 500,
                TargetStudyMinutes = (profile?.DailyStudyMinuteGoal ?? 60) * 7,
                CurrentQuestionCount = request.SolvedCount // İlk değerle başlat
            });
        }
        else
        {
            weeklyGoal.CurrentQuestionCount += request.SolvedCount;
            _manager.UserWeeklyGoalRepository.Update(weeklyGoal);
        }

        var newDetail = new QuestionStatDetail
        {
            CorrectCount = request.CorrectCount,
            SolvedCount = request.SolvedCount,
            AttemptedAt = now
        };

        if (uqs != null)
        {
            uqs.TotalSolvedCount += request.SolvedCount;
            uqs.TotalCorrectCount += request.CorrectCount;
            uqs.LastAttemptAt = now;
            newDetail.UserQuestionStatId = uqs.Id;
            _manager.QuestionStatDetailRepository.Create(newDetail);
        }
        else
        {
            _manager.UserQuestionStatRepository.Create(new UserQuestionStat
            {
                UserId = request.UserId,
                TopicId = request.TopicId,
                TotalCorrectCount = request.CorrectCount,
                TotalSolvedCount = request.SolvedCount,
                LastAttemptAt = now,
                QuestionStatDetails = new List<QuestionStatDetail> { newDetail }
            });
        }

        var profileStat = await _manager.ProfileStatRepository.GetByUserAsync(request.UserId, ct);
        if (profileStat != null)
        {
            profileStat.Score += (request.CorrectCount * topicPriority);

            var lastDate = profileStat.LastActivityDate?.Date ?? DateTime.MinValue.Date;

            if (lastDate != today)
            {
                profileStat.CurrentStreak = (lastDate == today.AddDays(-1)) ? profileStat.CurrentStreak + 1 : 1;

                if (profileStat.CurrentStreak > profileStat.BestStreak)
                    profileStat.BestStreak = profileStat.CurrentStreak;
            }

            profileStat.LastActivityDate = now;
            _manager.ProfileStatRepository.Update(profileStat);
        }

        _manager.UserActivityHistoryRepository.Create(new UserActivityHistory
        {
            UserId = request.UserId,
            ActivityType = ActivityType.SolvedAQuestion,
            Description = $"{request.SolvedCount} adet {topic?.Name ?? "Bilinmeyen Konu"} sorusu çözüldü.",
            ActivityDate = now
        });

        await _manager.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
