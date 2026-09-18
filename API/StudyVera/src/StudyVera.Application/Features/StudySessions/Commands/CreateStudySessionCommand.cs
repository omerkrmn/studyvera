using MediatR;
using StudyVera.Domain.Enums;

namespace StudyVera.Application.Features.StudySessions.Commands;

public record CreateStudySessionCommand(
    Guid UserId, 
    int DurationMinutes, 
    int? LessonId, 
    int? TopicId,
    string? Note,
    StudySessionType SessionType
) : IRequest<bool>;