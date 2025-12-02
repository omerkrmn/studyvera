using Mapster;
using MediatR;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserLessonProgresses;

public class AddRangeUserLessonProgressesHandler
: IRequestHandler<AddRangeUserLessonProgressesCommand, bool>
{
    private readonly IRepositoryManager _manager;

    public AddRangeUserLessonProgressesHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<bool> Handle(AddRangeUserLessonProgressesCommand request, CancellationToken ct)
    {
        if (request.LessonProgresses is null || !request.LessonProgresses.Any())
            throw new BadRequestException("No lesson progresses provided.");

        foreach (var lp in request.LessonProgresses)
        {
            var entity = lp.Adapt<UserLessonProgress>();
            entity.UserId = request.UserId;
            entity.LastUpdated = DateTime.Now;

            var activity = new UserActivityHistory
            {
                UserId = request.UserId,
                ActivityType = ActivityType.LessonProgressed,
                ActivityDate = DateTime.UtcNow,
                Description = $"User updated lesson progress for Topic ID {lp.TopicId} → Status: {lp.ProgressStatus}"
            };

            _manager.UserActivityHistoryRepository.Create(activity);
            _manager.UserLessonProgressRepository.Create(entity);
        }


        await _manager.SaveChangesAsync(ct);
        return true;
    }
}
