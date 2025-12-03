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

        
        var stats = await FindByCondition(uqs => uqs.UserId == userId, trackChanges: false)
                            // .Include(uqs => uqs.Topic) // Projeksiyon yaptığımız için gereksiz
                            .Select(uqs => new UserQuestionStatDto
                            {
                                Id = uqs.Id,
                                TopicId = uqs.TopicId,
                                Topic = new TopicDto
                                {
                                    Id = uqs.Topic.Id,
                                    LessonId = uqs.Topic.LessonId,
                                    Name = uqs.Topic.Name
                                },
                                TotalSolvedCount = uqs.TotalSolvedCount,
                                TotalCorrectCount = uqs.TotalCorrectCount,
                                LastAttemptAt = uqs.LastAttemptAt
                            })
                            .ToListAsync(ct);

        return stats;
    }

}
