using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyVera.Contract.Dtos.UserLessonProgress;
using StudyVera.Contract.Services;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Exceptions;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Services;

public class UserLessonProgressManager : IUserLessonProgressManager
{
    private readonly IRepositoryManager _manager;

    public UserLessonProgressManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<UserLessonProgress?> GetOne(Guid userId, int topicId, CancellationToken ct) =>
      await _manager.UserLessonProgressRepository.
            FindByCondition(ulp => ulp.UserId == userId && ulp.TopicId == topicId, false).
            SingleOrDefaultAsync(ct);


    public async Task<bool> AddAsync(Guid userId, int topicId, ProgressStatus progressStatus, CancellationToken ct)
    {
        var newProgress = new UserLessonProgress
        {
            UserId = userId,
            TopicId = topicId,
            ProgressStatus = progressStatus,
            LastUpdated = DateTime.UtcNow
        };
        var activity = new UserActivityHistory
        {
            UserId = userId,
            ActivityType = ActivityType.LessonProgressed,
            ActivityDate = DateTime.UtcNow,
            Description = $"Lesson progress inserted for Topic ID {topicId} → Status: {progressStatus}"
        };

        _manager.UserLessonProgressRepository.Create(newProgress);
        _manager.UserActivityHistoryRepository.Create(activity);
        await _manager.SaveChangesAsync(ct);
        return true;
    }
    public async Task<List<UserLessonProgressDto>> GetAllAsync(Guid userId, bool trackChanges, CancellationToken ct)
    {
        var progresses = await _manager.UserLessonProgressRepository
            .FindByCondition(x => x.UserId == userId, trackChanges)
            .Include(x => x.Topic)
                .ThenInclude(t => t.Lesson)
            .ToListAsync(ct);

        var result = progresses.Adapt<List<UserLessonProgressDto>>();

        return result;
    }

    public async Task<bool> IfExists(Guid userId, int topicId, CancellationToken ct)
    {
        return await _manager.UserLessonProgressRepository.ifExists(userId, topicId, ct);
    }

    public async Task<bool> UpdateAsync(int ulpId, Guid userId, ProgressStatus progressStatus, CancellationToken ct)
    {
        var progresses = await _manager.UserLessonProgressRepository.FindByCondition(x => x.Id == ulpId, true).SingleOrDefaultAsync();
        if (progresses == null)
            throw new NotFoundException($"{progresses.Id}'li progresss bulunamadı.");

        progresses.ProgressStatus = progressStatus;
        _manager.UserLessonProgressRepository.Update(progresses);

        var activity = new UserActivityHistory
        {
            UserId = userId,
            ActivityType = ActivityType.LessonProgressed,
            ActivityDate = DateTime.UtcNow,
            Description = $"Lesson progress updated for Topic ID {ulpId} → Status: {progressStatus}"
        };

        _manager.UserActivityHistoryRepository.Create(activity);
        await _manager.SaveChangesAsync();
        return true;
    }
}
