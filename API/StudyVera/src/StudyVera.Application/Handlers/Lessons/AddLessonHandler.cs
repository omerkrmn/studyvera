using MediatR;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.Lessons.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.Lessons;

public class AddLessonHandler : IRequestHandler<AddLessonCommand, int>
{
    private readonly IRepositoryManager _manager;

    public AddLessonHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<int> Handle(AddLessonCommand request, CancellationToken ct = default)
    {
        var exam = await _manager.ExamRepository.GetByIdAsync(request.ExamId, ct);
        var lessonIfExists = await _manager.LessonRepository.GetLessonByNameAndExamIdAsync(request.Name, request.ExamId, false, ct);
        if (lessonIfExists != null) throw new NotFoundException("lesson is have found");

        var lesson = new Lesson()
        {
            ExamId = request.ExamId,
            Name = request.Name
        };
        _manager.LessonRepository.Create(lesson);
        await _manager.SaveChangesAsync(ct);
        return lesson.Id;
    }
}
