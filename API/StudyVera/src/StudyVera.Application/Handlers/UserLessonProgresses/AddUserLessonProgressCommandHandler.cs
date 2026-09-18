using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserLessonProgresses
{
    public class AddUserLessonProgressCommandHandler : IRequestHandler<AddUserLessonProgressCommand, Unit>
    {
        // buradaki kodu değiştiriyorum. 

        private readonly IRepositoryManager _manager;

        public AddUserLessonProgressCommandHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<Unit> Handle(AddUserLessonProgressCommand request, CancellationToken cancellationToken)
        {
            var exists = await _manager.UserLessonProgressRepository.ExistsAsync(request.UserId, request.TopicId, cancellationToken);
            if (exists)
                throw new ConflictException("Böyle bir veri zaten kayıtlı.");

            var entity = request.Adapt<UserLessonProgress>();

            entity.LastUpdated = DateTime.UtcNow;
            _manager.UserLessonProgressRepository.Create(entity);

            var profileStat = await _manager.ProfileStatRepository.GetByUserAsync(request.UserId, cancellationToken);
            if (profileStat != null)
            {
                var today = DateTime.UtcNow.Date;
                var lastActivity = profileStat.LastActivityDate?.Date;

                if (lastActivity == today.AddDays(-1))
                {
                    profileStat.CurrentStreak += 1;
                    profileStat.LastActivityDate = DateTime.UtcNow;
                }
                else
                {
                    profileStat.CurrentStreak = 1;
                    profileStat.LastActivityDate = DateTime.UtcNow;
                }

                if (profileStat.CurrentStreak > profileStat.BestStreak)
                    profileStat.BestStreak = profileStat.CurrentStreak;
            }


            _manager.UserActivityHistoryRepository.Create(new()
            {
                UserId = request.UserId,
                ActivityType = ActivityType.LessonProgressed,
                LessonId = request.TopicId,
                Description = $"Kullanıcı {request.TopicId} numaralı konuda güncelleme yaptı {request.ProgressStatus}",
                ActivityDate = DateTime.UtcNow,
            });

            if (request.DurationMinutes.HasValue && request.DurationMinutes.Value > 0)
            {
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

            await _manager.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
