using Mapster;
using MediatR;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserLessonProgresses
{
    public class AddUserLessonProgressCommandHandler : IRequestHandler<AddUserLessonProgressCommand, Unit>
    {
        // buradaki kodu değiştiriyorum. 

        private readonly IRepositoryManager _manager;

        public AddUserLessonProgressCommandHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<Unit> Handle(AddUserLessonProgressCommand request, CancellationToken cancellationToken)
        {
            var exists = await _manager.UserLessonProgressRepository.ExistsAsync(request.UserId, request.TopicId, cancellationToken);
            if (exists)
                throw new ConflictException("Böyle bir veri zaten kayıtlı.");

            var entity = request.Adapt<UserLessonProgress>();

            entity.LastUpdated = DateTime.UtcNow;
            _manager.UserLessonProgressRepository.Create(entity);

            await _manager.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

}
