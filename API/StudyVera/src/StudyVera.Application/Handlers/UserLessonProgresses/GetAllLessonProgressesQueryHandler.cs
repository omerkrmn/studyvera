using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos;
using StudyVera.Application.Dtos.UserLessonProgress;
using StudyVera.Application.Features.UserLessonProgresses.Queries;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserLessonProgresses
{
    public class GetAllLessonProgressesQueryHandler : IRequestHandler<GetAllLessonProgressesQuery, List<UserLessonProgressDto>>
    {
        private readonly IRepositoryManager _manager;

        public GetAllLessonProgressesQueryHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<UserLessonProgressDto>> Handle(GetAllLessonProgressesQuery request, CancellationToken cancellationToken)
        {
            var userLessonProgresses =
                await _manager.UserLessonProgressRepository
                .FindByCondition(ulp => ulp.UserId == request.UserId, false)
                .Include(a => a.Topic)
                .Select(ulp => new UserLessonProgressDto()
                {
                    Id = ulp.Id,
                    TopicId = ulp.TopicId,
                    ProgressStatus = ulp.ProgressStatus,
                    Topic = new TopicDto
                    {
                        Id = ulp.Topic.Id,
                        LessonId = ulp.Topic.LessonId,
                        Name = ulp.Topic.Name
                    }
                })

                .ToListAsync(cancellationToken);

            var response = userLessonProgresses.Adapt<List<UserLessonProgressDto>>();
            return response;
        }
    }

}
