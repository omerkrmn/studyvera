using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;


namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class LessonScheduleRepository : RepositoryBase<LessonSchedule>, ILessonScheduleRepository
{
    public LessonScheduleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<LessonSchedule>> GetWeeklyScheduleAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.LessonSchedules
            .Where(dto => dto.UserId == userId)
            .OrderBy(dto => dto.DayOfWeek)
            .ThenBy(dto => dto.StartTime)
            .Select(dto => new LessonSchedule
            {
                UserId = dto.UserId,
                LessonId = dto.LessonId,
                TopicId = dto.TopicId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                LastUpdatedAt = dto.LastUpdatedAt
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<LessonSchedule?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.LessonSchedules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
