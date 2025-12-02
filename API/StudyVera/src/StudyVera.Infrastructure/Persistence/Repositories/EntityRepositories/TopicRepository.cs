using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class TopicRepository : RepositoryBase<Topic>, ITopicRepository
{
    public TopicRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Topic>> GetAllByLessonIdAsync(TargetExam exam, int lessonId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Topic>> GetAllByTargetAsync(TargetExam exam, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
