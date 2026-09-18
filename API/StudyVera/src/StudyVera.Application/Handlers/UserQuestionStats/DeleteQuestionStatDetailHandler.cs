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

public class DeleteQuestionStatDetailHandler : IRequestHandler<DeleteQuestionStatDetailCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public DeleteQuestionStatDetailHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(DeleteQuestionStatDetailCommand request, CancellationToken ct)
    {
        var detail = await _manager.QuestionStatDetailRepository
            .FindByCondition(d => d.Id == request.QuestionStatDetailId, true)
            .Include(d => d.userQuestionStat)
            .FirstOrDefaultAsync(ct);

        if (detail == null)
            throw new NotFoundException("Kayıt bulunamadı.");

        if (detail.userQuestionStat.UserId != request.UserId)
            throw new UnauthorizedAccessException("Bu kaydı silme yetkiniz yok.");

        var uqs = detail.userQuestionStat;
        
        var topic = await _manager.TopicRepository
            .FindByCondition(t => t.Id == uqs.TopicId, false)
            .SingleOrDefaultAsync(ct);

        // İstatistikleri geri al
        uqs.TotalSolvedCount -= detail.SolvedCount;
        uqs.TotalCorrectCount -= detail.CorrectCount;
        uqs.TotalTimeSpentInMinutes -= detail.DurationMinutes ?? 0;

        if (uqs.TotalSolvedCount < 0) uqs.TotalSolvedCount = 0;
        if (uqs.TotalCorrectCount < 0) uqs.TotalCorrectCount = 0;
        if (uqs.TotalTimeSpentInMinutes < 0) uqs.TotalTimeSpentInMinutes = 0;

        // Profil Puanını geri al
        int topicPriority = topic?.Priority ?? 3;
        var profileStat = await _manager.ProfileStatRepository.GetByUserAsync(request.UserId, ct);
        if (profileStat != null)
        {
            profileStat.Score -= (detail.CorrectCount * topicPriority);
            if (profileStat.Score < 0) profileStat.Score = 0;
        }

        // Geçmiş kaydı oluştur
        _manager.UserActivityHistoryRepository.Create(new UserActivityHistory
        {
            UserId = request.UserId,
            ActivityType = ActivityType.SolvedAQuestion,
            TopicId = uqs.TopicId,
            LessonId = topic?.LessonId,
            Description = $"Kayıt İptali: {detail.SolvedCount} soruluk kayıt silindi.",
            ActivityDate = DateTime.UtcNow
        });

        // Detayı sil
        _manager.QuestionStatDetailRepository.Delete(detail);

        await _manager.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
