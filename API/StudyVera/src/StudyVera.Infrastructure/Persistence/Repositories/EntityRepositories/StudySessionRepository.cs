using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Infrastructure.Persistence.Repositories;

public class StudySessionRepository : RepositoryBase<StudySession>, IStudySessionRepository
{
    public StudySessionRepository(AppDbContext context) : base(context)
    {
    }

}