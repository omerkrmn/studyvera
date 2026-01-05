using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class ExamRepository : RepositoryBase<Exam>, IExamRepository
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Exam> GetByIdAsync(int id, CancellationToken ct)
    {
        var exam = await _context.Exams.FindAsync(id);
        if (exam == null)
            throw new NotFoundException($"Exam with id {id} not found.");
        return exam;
    }
}
