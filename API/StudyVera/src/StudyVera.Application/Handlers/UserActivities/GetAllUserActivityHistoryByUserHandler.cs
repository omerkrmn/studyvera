using Mapster;
using MediatR;
using StudyVera.Application.Dtos.UserActivityHistory;
using StudyVera.Application.Features.UserActivities.Queries;
using StudyVera.Domain.Interfaces;

namespace StudyVera.Application.Handlers.UserActivities
{
    public class GetAllUserActivityHistoryByUserHandler : IRequestHandler<GetAllUserActivityHistoryByUserQuery, List<UserActivityHistoryDto>>
    {
        private readonly IRepositoryManager _manager;

        public GetAllUserActivityHistoryByUserHandler(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task<List<UserActivityHistoryDto>> Handle(GetAllUserActivityHistoryByUserQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var userActivities = await _manager.UserActivityHistoryRepository.GetAllByUserAsycn(request.UserId, cancellationToken);
            return userActivities.Adapt<List<UserActivityHistoryDto>>();
        }
    }

}
