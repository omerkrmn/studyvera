using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Models;
using StudyVera.Application.Dtos;
using StudyVera.Application.Dtos.QuestionStatDetails;
using StudyVera.Application.Dtos.Topic;
using StudyVera.Application.Dtos.UserQuestionStats;
using StudyVera.Application.Features.UserQuestionStats.Queries;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserQuestionStats
{
    public class GetAllUserQuestionStatsByUserHandler : IRequestHandler<GetAllUserQuestionStatsByUserQuery, List<UserQuestionStatDto>>
    {
        private readonly IRepositoryManager _manager;

        public GetAllUserQuestionStatsByUserHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<UserQuestionStatDto>> Handle(GetAllUserQuestionStatsByUserQuery request, CancellationToken cancellationToken)
        {
            return await _manager.UserQuestionStatRepository
                .FindByCondition(uqs => uqs.UserId == request.UserId, trackChanges: false)
                .Select(uqs => new UserQuestionStatDto
                {
                    Id = uqs.Id,
                    TopicId = uqs.TopicId,
                    Topic = new TopicWithoutIdColumnDto
                    {
                        LessonId = uqs.Topic.LessonId,
                        Name = uqs.Topic.Name,
                        Priority = uqs.Topic.Priority
                    },
                    TotalSolvedCount = uqs.TotalSolvedCount,
                    TotalCorrectCount = uqs.TotalCorrectCount,
                    LastAttemptAt = uqs.LastAttemptAt
                })
                .ToListAsync(cancellationToken);
        }
    }


}
