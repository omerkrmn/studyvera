using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using StudyVera.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class LessonRepository : RepositoryBase<Lesson>, ILessonRepository
{
    public LessonRepository(AppDbContext context) : base(context)
    {
    }

    public Task AddLessonAsync(Lesson lesson, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<List<Lesson>> GetByTargetExamAsync(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Lesson?> GetLessonByNameAndExamIdAsync(string name, int id, bool trackChanges, CancellationToken ct)
    {
        return await FindByCondition(l => l.Name == name && l.ExamId == id, trackChanges)
            .FirstOrDefaultAsync();
    }
}
