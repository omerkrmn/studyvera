using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class TopicRepository : RepositoryBase<Topic>, ITopicRepository
{
    public TopicRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Topic>> GetAllByLessonIdAsync(ExamTarget exam, int lessonId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<Topic>> GetAllByTargetAsync(ExamTarget exam, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
