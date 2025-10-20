using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IUserLessonProgressRepository : IRepository<UserLessonProgress>
{
    public Task<List<UserLessonProgress>> GetAllByProgressStatusAsync(Guid userId, int progressStatus, CancellationToken ct);
    public Task<List<UserLessonProgress>> GetAllByProgressStatusAndLastUpdatedBeforeAsync(Guid userId, int progressStatus, DateTime lastUpdated, CancellationToken ct);

}
