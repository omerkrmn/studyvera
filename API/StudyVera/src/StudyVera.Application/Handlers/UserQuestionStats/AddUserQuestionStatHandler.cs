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
        var uqs = await _manager.UserQuestionStatRepository.FindByCondition(
            uqs => uqs.UserId == request.UserId && uqs.TopicId == request.TopicId,
            true 
        ).FirstOrDefaultAsync(ct);

        var newDetail = new QuestionStatDetail
        {
            CorrectCount = request.CorrectCount,
            SolvedCount = request.SolvedCount,
            AttemptedAt = DateTime.UtcNow,
        };

        if (uqs is not null)
        {
            uqs.TotalSolvedCount += request.SolvedCount;
            uqs.TotalCorrectCount += request.CorrectCount;
            uqs.LastAttemptAt = DateTime.UtcNow;
            newDetail.UserQuestionStatId = uqs.Id;
            _manager.QuestionStatDetailRepository.Create(newDetail);
        }
        else
        {
            var newStat = new UserQuestionStat
            {
                UserId = request.UserId,
                TopicId = request.TopicId,
                TotalCorrectCount = request.CorrectCount,
                TotalSolvedCount = request.SolvedCount,
                LastAttemptAt = DateTime.UtcNow,
         
                QuestionStatDetails = new List<QuestionStatDetail> { newDetail }
            };

            _manager.UserQuestionStatRepository.Create(newStat);
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
