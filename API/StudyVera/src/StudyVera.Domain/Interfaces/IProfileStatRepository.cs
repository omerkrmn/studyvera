using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Interfaces;

public interface IProfileStatRepository : IRepository<ProfileStat>
{
    public Task<int> GetScoreByUser(Guid userId, CancellationToken ct);
    public void UpdateScore(Guid userId, ProgressStatus progressStatus, CancellationToken ct);

}
