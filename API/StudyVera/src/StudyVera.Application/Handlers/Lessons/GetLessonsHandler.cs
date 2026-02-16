using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Dtos.Lessons;
using StudyVera.Application.Features.Lessons.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Lessons;

public class GetLessonsHandler : IRequestHandler<GetLessonsQuery, List<LessonDto>>
{
    private readonly IRepositoryManager _manager;

    public GetLessonsHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<List<LessonDto>> Handle(GetLessonsQuery request, CancellationToken cancellationToken)
    {
        var lessons = await _manager.LessonRepository
                    .FindByCondition(ul => ul.ExamId == (int)request.TargetExam, false)
                    .ProjectToType<LessonDto>()
                    .ToListAsync(cancellationToken);
        return lessons;
    }
}
