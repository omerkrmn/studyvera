using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserLessonProgresses;

public class UpdateUserLessonProgressHandler : IRequestHandler<UpdateUserLessonProgressCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public UpdateUserLessonProgressHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(UpdateUserLessonProgressCommand request, CancellationToken cancellationToken)
    {
        var ulp = await _manager.UserLessonProgressRepository
            .FindByCondition(u => u.UserId == request.UserId && u.TopicId == request.TopicId, trackChanges: true)
            .FirstOrDefaultAsync(cancellationToken);

        if (ulp == null)
            throw new NotFoundException($"UserLessonProgress not found.");


        ulp.ProgressStatus = request.ProgressStatus;
        ulp.LastUpdated = DateTime.UtcNow;

        _manager.UserActivityHistoryRepository.Create(new()
        {
            UserId = request.UserId,
            ActivityType = ActivityType.LessonCompleted,
            Description = $"User updated lesson progress for Topic ID {ulp.TopicId} → Status: {ulp.ProgressStatus}"
        });

        _manager.UserLessonProgressRepository.Update(ulp);
        await _manager.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}