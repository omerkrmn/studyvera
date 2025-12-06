using Mapster;
using MediatR;
using StudyVera.Application.Features.LessonSchedules.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.LessonSchedules;

public class AddLessonScheduleHandler : IRequestHandler<AddLessonScheduleCommand, Unit>
{
    private readonly IRepositoryManager _manager;

    public AddLessonScheduleHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Unit> Handle(AddLessonScheduleCommand request, CancellationToken cancellationToken)
    {
        _manager.LessonScheduleRepository.Create(request.Adapt<LessonSchedule>());
        await _manager.SaveChangesAsync();
        return Unit.Value;
    }
}
