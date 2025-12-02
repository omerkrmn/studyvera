using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserLessonProgresses
{
    public class UpdateUserLessonProgressesHandler : IRequestHandler<UpdateUserLessonProgressCommand, Unit>
    {
        private readonly IRepositoryManager _manager;

        public UpdateUserLessonProgressesHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<Unit> Handle(UpdateUserLessonProgressCommand request, CancellationToken cancellationToken)
        {
            var ulp = await _manager
                .UserLessonProgressRepository
                .FindByCondition(u => u.UserId == request.UserId && u.TopicId == request.TopicId, true)
                .FirstOrDefaultAsync(cancellationToken);

            if (ulp == null)
                throw new NotFoundException("User lesson progress not found");

            ulp.ProgressStatus = request.ProgressStatus;
            ulp.LastUpdated = DateTime.UtcNow;
            _manager.UserLessonProgressRepository.Update(ulp);
            await _manager.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
