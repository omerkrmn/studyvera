using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class QuestionStatDetailRepository : RepositoryBase<QuestionStatDetail>, IQuestionStatDetailRepository
{
    public QuestionStatDetailRepository(AppDbContext context) : base(context)
    {
    }

    public (Task<int> TotalSolvedCount, Task<int> TotalCorrectCount) GetSum(int questionStatDetailId)
    {
        return (_context.QuestionStatDetails
                .Select(q => new { q.Id, q.SolvedCount })
                .Where(q => q.Id == questionStatDetailId)
                .SumAsync(q => q.SolvedCount),

                _context.QuestionStatDetails
                .Select(q => new { q.Id, q.CorrectCount })
                .Where(q => q.Id == questionStatDetailId)
                .SumAsync(q => q.CorrectCount));
    }
}
