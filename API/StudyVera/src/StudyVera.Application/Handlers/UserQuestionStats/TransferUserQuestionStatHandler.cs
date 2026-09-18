using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserQuestionStats.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserQuestionStats;

public class TransferUserQuestionStatHandler : IRequestHandler<TransferUserQuestionStatCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public TransferUserQuestionStatHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(TransferUserQuestionStatCommand request, CancellationToken ct)
    {
        var detail = await _manager.QuestionStatDetailRepository
            .FindByCondition(d => d.Id == request.QuestionStatDetailId, true)
            .Include(d => d.userQuestionStat)
            .FirstOrDefaultAsync(ct);

        if (detail == null)
            throw new NotFoundException("Kayıt bulunamadı.");

        if (detail.userQuestionStat.UserId != request.UserId)
            throw new UnauthorizedAccessException("Bu kaydı değiştirme yetkiniz yok.");

        var oldUqs = detail.userQuestionStat;
        
        if (oldUqs.TopicId == request.NewTopicId)
            return Unit.Value; // Zaten aynı konu, işlem yapmaya gerek yok.

        var oldTopic = await _manager.TopicRepository
            .FindByCondition(t => t.Id == oldUqs.TopicId, false)
            .SingleOrDefaultAsync(ct);

        var newTopic = await _manager.TopicRepository
            .FindByCondition(t => t.Id == request.NewTopicId, false)
            .SingleOrDefaultAsync(ct);

        if (newTopic == null)
            throw new NotFoundException("Hedef konu bulunamadı.");

        // Eski kayıttan düşür
        oldUqs.TotalSolvedCount -= detail.SolvedCount;
        oldUqs.TotalCorrectCount -= detail.CorrectCount;
        oldUqs.TotalTimeSpentInMinutes -= detail.DurationMinutes ?? 0;

        if (oldUqs.TotalSolvedCount < 0) oldUqs.TotalSolvedCount = 0;
        if (oldUqs.TotalCorrectCount < 0) oldUqs.TotalCorrectCount = 0;
        if (oldUqs.TotalTimeSpentInMinutes < 0) oldUqs.TotalTimeSpentInMinutes = 0;

        // Yeni konuyu bul veya oluştur
        var newUqs = await _manager.UserQuestionStatRepository
            .FindByCondition(u => u.UserId == request.UserId && u.TopicId == request.NewTopicId, true)
            .FirstOrDefaultAsync(ct);

        if (newUqs == null)
        {
            newUqs = new UserQuestionStat
            {
                UserId = request.UserId,
                TopicId = request.NewTopicId,
                TotalCorrectCount = detail.CorrectCount,
                TotalSolvedCount = detail.SolvedCount,
                TotalTimeSpentInMinutes = detail.DurationMinutes ?? 0,
                LastAttemptAt = detail.AttemptedAt,
                QuestionStatDetails = new List<QuestionStatDetail>()
            };
            _manager.UserQuestionStatRepository.Create(newUqs);
        }
        else
        {
            newUqs.TotalSolvedCount += detail.SolvedCount;
            newUqs.TotalCorrectCount += detail.CorrectCount;
            newUqs.TotalTimeSpentInMinutes += detail.DurationMinutes ?? 0;
            if (detail.AttemptedAt > newUqs.LastAttemptAt)
            {
                newUqs.LastAttemptAt = detail.AttemptedAt;
            }
        }

        // Puan düzeltmesi (Eğer konuların öncelikleri farklıysa)
        int oldPriority = oldTopic?.Priority ?? 3;
        int newPriority = newTopic.Priority;

        if (oldPriority != newPriority)
        {
            var profileStat = await _manager.ProfileStatRepository.GetByUserAsync(request.UserId, ct);
            if (profileStat != null)
            {
                profileStat.Score -= (detail.CorrectCount * oldPriority);
                profileStat.Score += (detail.CorrectCount * newPriority);
                if (profileStat.Score < 0) profileStat.Score = 0;
            }
        }

        // Detayı yeni uqs'e bağla
        // Eğer newUqs yeni oluşturulduysa ID'si veritabanı save sonrası belli olur.
        // Bu yüzden eğer oluşturulduysa listesine ekleyip eski uqs'ten silebiliriz ya da FK ayarlayabiliriz.
        // EF Core track ettiği için nesne referansını değiştirmek en güvenlisidir.
        detail.userQuestionStat = newUqs;

        // Geçmiş kaydı oluştur
        _manager.UserActivityHistoryRepository.Create(new UserActivityHistory
        {
            UserId = request.UserId,
            ActivityType = ActivityType.SolvedAQuestion,
            TopicId = request.NewTopicId,
            LessonId = newTopic.LessonId,
            Description = $"Kayıt Düzeltme: {detail.SolvedCount} soru '{oldTopic?.Name}' konusundan '{newTopic.Name}' konusuna taşındı.",
            ActivityDate = DateTime.UtcNow
        });

        await _manager.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
