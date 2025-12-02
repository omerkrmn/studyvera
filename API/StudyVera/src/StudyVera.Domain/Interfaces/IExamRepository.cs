using StudyVera.Domain.Entities;

namespace StudyVera.Domain.Interfaces;

public interface IExamRepository : IRepository<Exam>
{
    Task<Exam> GetByIdAsync(int id, CancellationToken ct);
}
