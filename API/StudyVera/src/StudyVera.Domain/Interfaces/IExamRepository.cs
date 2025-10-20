using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IExamRepository : IRepository<Exam>
{
    Task<Exam> GetByIdAsync(int id, CancellationToken ct);

}
