using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Models;
using StudyVera.Application.Dtos;
using StudyVera.Application.Dtos.UserQuestionStats;
using StudyVera.Application.Features.UserQuestionStats.Queries;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserQuestionStats
{
    public class GetAllUserQuestionStatsByUserHandler : IRequestHandler<GetAllUserQuestionStatsByUserQuery, PagedList<UserQuestionStatDto>>
    {
        private readonly IRepositoryManager _manager;

        public GetAllUserQuestionStatsByUserHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<PagedList<UserQuestionStatDto>> Handle(
        GetAllUserQuestionStatsByUserQuery request,
        CancellationToken cancellationToken)
        {
            var baseQuery = _manager.UserQuestionStatRepository
                                    .FindByCondition(uqs => uqs.UserId == request.UserId, trackChanges: false)
                                    .AsQueryable();

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            var pagedAndProjectedQuery = baseQuery
                                         .Skip(request.PageSize * (request.PageNumber - 1))
                                         .Take(request.PageSize)
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
                                             SolvedCount = uqs.SolvedCount,
                                             CorrectCount = uqs.CorrectCount,
                                             WrongCount = uqs.WrongCount,
                                             AccuracyRate = uqs.AccuracyRate,
                                             LastAttemptAt = uqs.LastAttemptAt
                                         });

            var items = await pagedAndProjectedQuery.ToListAsync(cancellationToken);
            return new PagedList<UserQuestionStatDto>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }

}
