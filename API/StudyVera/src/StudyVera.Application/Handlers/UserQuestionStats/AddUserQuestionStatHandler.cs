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
        if (request.SolvedCount < request.CorrectCount)
            throw new BadRequestException("SolvedCount cannot be less than CorrectCount.");
        
        var stat = await _manager.UserQuestionStatRepository
            .FindByCondition(x => x.UserId == request.UserId && x.TopicId == request.TopicId, trackChanges: false)
            .FirstOrDefaultAsync(ct);

        if (stat is null)
        {
            var newStat = request.Adapt<UserQuestionStat>();
            Console.WriteLine($"Creating new UserQuestionStat for UserId: {request.UserId}, TopicId: {request.TopicId}");
            newStat.LastAttemptAt = DateTime.UtcNow;
            _manager.UserQuestionStatRepository.Create(newStat);
        }
        else
        {
            stat.SolvedCount += request.SolvedCount;
            stat.CorrectCount += request.CorrectCount;
            stat.LastAttemptAt = DateTime.UtcNow;
            _manager.UserQuestionStatRepository.Update(stat);
        }

        _manager.UserActivityHistoryRepository.Create(new()
        {
            UserId = request.UserId,
            ActivityType = ActivityType.SolvedAQuestion,
            Description = $"Kullanıcı {request.TopicId} numaralı konuda {request.SolvedCount} soru çözdü.",
            ActivityDate = DateTime.UtcNow,
        });

        await _manager.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
