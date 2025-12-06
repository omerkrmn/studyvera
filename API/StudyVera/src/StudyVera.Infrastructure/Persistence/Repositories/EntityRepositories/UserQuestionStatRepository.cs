using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Models;
using StudyVera.Application.Dtos;
using StudyVera.Application.Dtos.UserQuestionStats;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Infrastructure.Persistence.Repositories.EntityRepositories;

public class UserQuestionStatRepository : RepositoryBase<UserQuestionStat>, IUserQuestionStatRepository
{
    public UserQuestionStatRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<UserQuestionStatDto>> GetAllByUser(Guid userId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

}
