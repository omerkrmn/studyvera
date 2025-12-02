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
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.DayOfWeek)
            .ThenBy(x => x.StartTime)
            .ToListAsync(cancellationToken);
    }

    public async Task<LessonSchedule?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.LessonSchedules.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
