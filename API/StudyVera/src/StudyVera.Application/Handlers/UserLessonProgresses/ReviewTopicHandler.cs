using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
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