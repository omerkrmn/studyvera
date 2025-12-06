using Mapster;
using MediatR;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Dtos.LessonSchedules;
using StudyVera.Application.Features.LessonSchedules.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.LessonSchedules;

public class GetLessonScheduleHandler : IRequestHandler<GetLessonScheduleQuery, List<LessonScheduleDto>>
{

    private readonly IRepositoryManager _manager;

    public GetLessonScheduleHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<List<LessonScheduleDto>> Handle(GetLessonScheduleQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _manager.LessonScheduleRepository.GetWeeklyScheduleAsync(request.UserId,cancellationToken);
        if (!schedule.Any())
            throw new NotFoundException("lesson schedule is not found!");
        
        return schedule.Adapt<List<LessonScheduleDto>>();
    }
}
