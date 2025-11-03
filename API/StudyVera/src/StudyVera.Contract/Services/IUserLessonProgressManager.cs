using StudyVera.Contract.Dtos.UserLessonProgress;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Contract.Services;

public interface IUserLessonProgressManager
{
    public Task<List<UserLessonProgressDto>> GetAllAsync(Guid userId, bool trackChanges, CancellationToken ct);
    Task<bool> AddAsync(Guid userId, int topicId, ProgressStatus progressStatus, CancellationToken ct);
    Task<bool> UpdateAsync(int ulpId, Guid userId, ProgressStatus progressStatus, CancellationToken ct);
    Task<bool> IfExists(Guid userId, int ulpId, CancellationToken ct);
    Task<UserLessonProgress?> GetOne(Guid userId, int topicId, CancellationToken ct);
}
