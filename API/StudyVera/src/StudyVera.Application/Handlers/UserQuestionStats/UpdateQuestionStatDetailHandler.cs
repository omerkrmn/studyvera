using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserQuestionStats.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserQuestionStats;

public class UpdateQuestionStatDetailHandler : IRequestHandler<UpdateQuestionStatDetailCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public UpdateQuestionStatDetailHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(UpdateQuestionStatDetailCommand request, CancellationToken ct)
    {
        var detail = await _manager.QuestionStatDetailRepository
            .FindByCondition(d => d.Id == request.QuestionStatDetailId, true)
            .Include(d => d.userQuestionStat)
            .FirstOrDefaultAsync(ct);

        if (detail == null)
            throw new NotFoundException("Kayıt bulunamadı.");

        if (detail.userQuestionStat.UserId != request.UserId)
            throw new UnauthorizedAccessException("Bu kaydı düzenleme yetkiniz yok.");

        var uqs = detail.userQuestionStat;

        var topic = await _manager.TopicRepository
            .FindByCondition(t => t.Id == uqs.TopicId, false)
            .SingleOrDefaultAsync(ct);

        // Değişim miktarlarını hesapla (Yeni Değer - Eski Değer)
        int diffSolved = request.SolvedCount - detail.SolvedCount;
        int diffCorrect = request.CorrectCount - detail.CorrectCount;
        int diffDuration = (request.DurationMinutes ?? 0) - (detail.DurationMinutes ?? 0);

        // Detayı güncelle
        detail.SolvedCount = request.SolvedCount;
        detail.CorrectCount = request.CorrectCount;
        detail.DurationMinutes = request.DurationMinutes;

        // UQS İstatistiklerini güncelle
        uqs.TotalSolvedCount += diffSolved;
        uqs.TotalCorrectCount += diffCorrect;
        uqs.TotalTimeSpentInMinutes += diffDuration;

        if (uqs.TotalSolvedCount < 0) uqs.TotalSolvedCount = 0;
        if (uqs.TotalCorrectCount < 0) uqs.TotalCorrectCount = 0;
        if (uqs.TotalTimeSpentInMinutes < 0) uqs.TotalTimeSpentInMinutes = 0;

        // Profil Puanını güncelle
        int topicPriority = topic?.Priority ?? 3;
        var profileStat = await _manager.ProfileStatRepository.GetByUserAsync(request.UserId, ct);
        if (profileStat != null)
        {
            // diffCorrect pozitifse puan artar, negatifse azalır
            profileStat.Score += (diffCorrect * topicPriority);
            if (profileStat.Score < 0) profileStat.Score = 0;
        }

        // Geçmiş kaydı oluştur
        _manager.UserActivityHistoryRepository.Create(new UserActivityHistory
        {
            UserId = request.UserId,
            ActivityType = ActivityType.SolvedAQuestion,
            TopicId = uqs.TopicId,
            LessonId = topic?.LessonId,
            Description = $"Kayıt Düzenlendi: İstatistik güncellendi. Yeni değer: {request.SolvedCount} soru.",
            ActivityDate = DateTime.UtcNow
        });

        await _manager.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
