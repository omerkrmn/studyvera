using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities.Mock;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserMockExamRepository : RepositoryBase<UserMockExam>, IUserMockExamRepository
{
    public UserMockExamRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<UserMockExam>> GetUserExamHistoryAsync(Guid userId, CancellationToken ct)
    {
        return await _context.UserMockExams
            .AsNoTracking() 
            .Where(ume => ume.UserId == userId)
            .OrderByDescending(ume => ume.ExamDate) 
            .ToListAsync(ct);
    }

    public async Task<UserMockExam?> GetWithDetailsAsync(int id, CancellationToken ct)
    {
        return await _context.UserMockExams
            .Include(ume => ume.Exam)
            .Include(ume => ume.Details)
                .ThenInclude(d => d.Lesson)
            .FirstOrDefaultAsync(ume => ume.Id == id, ct);
    }
}