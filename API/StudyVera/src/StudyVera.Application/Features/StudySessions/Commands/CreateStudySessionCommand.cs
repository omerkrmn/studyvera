using MediatR;

namespace StudyVera.Application.Features.StudySessions.Commands;

public record CreateStudySessionCommand(
    Guid UserId, 
    int DurationMinutes, 
    int? LessonId, 
    int? TopicId,
    string? Note
) : IRequest<bool>;