using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface ILessonRepository
{
    public Task<List<Lesson>> GetByTargetExam(Guid userId, CancellationToken ct);
    public void AddLesson(Lesson lesson, CancellationToken ct);
}
