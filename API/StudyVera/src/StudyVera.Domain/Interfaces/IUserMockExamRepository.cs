using StudyVera.Domain.Entities.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IUserMockExamRepository : IRepository<UserMockExam>
{
    public Task<UserMockExam?> GetWithDetailsAsync(int id,CancellationToken ct);
    public Task<List<UserMockExam>> GetUserExamHistoryAsync(Guid userId,CancellationToken ct);
}
