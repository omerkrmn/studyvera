using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos;
using StudyVera.Application.Features.Topics.Queries;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.Topics
{

    public class GetAllTopicsHandler : IRequestHandler<GetAllTopicsQuery, List<TopicDto>>
    {
        private readonly IRepositoryManager _manager;

        public GetAllTopicsHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<TopicDto>> Handle(GetAllTopicsQuery request, CancellationToken ct)
        {
            var query = _manager.TopicRepository
                                .FindByCondition(t => t.Lesson.ExamId == (int)request.TargetExam, false);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim();
                query = query.Where(t => t.Name.Contains(term));
            }
            return await query
                .OrderBy(t => t.Priority)
                .ProjectToType<TopicDto>()
                .ToListAsync(ct);
        }
    }
}
