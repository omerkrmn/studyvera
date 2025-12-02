using MediatR;
using StudyVera.Application.Dtos.UserLessonProgress;

namespace StudyVera.Application.Features.UserLessonProgresses.Queries
{
    public class GetAllLessonProgressesQuery : IRequest<List<UserLessonProgressDto>>
    {
        public Guid UserId { get; set; }
    }

}
