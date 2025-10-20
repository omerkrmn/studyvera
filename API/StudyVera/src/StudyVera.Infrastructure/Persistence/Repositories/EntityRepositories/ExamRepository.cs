using StudyVera.Domain.Entities;
using StudyVera.Domain.Exceptions;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class ExamRepository : RepositoryBase<Exam>, IExamRepository
{
    public ExamRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Exam> GetByIdAsync(int id, CancellationToken ct)
    {
        var exam =await _context.Exams.FindAsync(id);
        if (exam == null)
            throw new NotFoundException($"Exam with id {id} not found.");
        return exam;
    }
}
