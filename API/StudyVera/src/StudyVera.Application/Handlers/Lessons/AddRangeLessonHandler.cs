using MediatR;
using StudyVera.Application.Features.Lessons.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Lessons;

public class AddRangeLessonHandler : IRequestHandler<AddRangeLessonCommand, List<int>>
{
    private readonly IRepositoryManager _manager;

    public AddRangeLessonHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<List<int>> Handle(AddRangeLessonCommand request, CancellationToken cancellationToken)
    {
        if (request.List is null || !request.List.Any())
            throw new ArgumentException("Lesson listesi boş olamaz.");

        var newLessons = request.List.Select(item => new Lesson
        {
            Name = item.Key,
            ExamId = item.Value,
        }).ToList();

        for (int i = 0; i < newLessons.Count; i++)
        {
             _manager.LessonRepository.Create(newLessons[i]);
        }

        await _manager.SaveChangesAsync(cancellationToken);

        return newLessons.Select(x => x.Id).ToList();
    }
}
