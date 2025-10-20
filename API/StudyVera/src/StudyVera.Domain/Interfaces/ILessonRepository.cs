using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface ILessonRepository : IRepository<Lesson>
{
    public Task<List<Lesson>> GetByTargetExamAsync(Guid userId, CancellationToken ct);
    public Task AddLessonAsync(Lesson lesson, CancellationToken ct);
    public Task<Lesson?> GetLessonByNameAndExamIdAsync(string name,int id,bool trackChanges, CancellationToken ct);
}
