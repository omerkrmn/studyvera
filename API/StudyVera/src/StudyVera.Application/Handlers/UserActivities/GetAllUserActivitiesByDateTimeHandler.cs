using MediatR;
using StudyVera.Application.Features.UserActivities.Queries;
using StudyVera.Domain.Interfaces;


namespace StudyVera.Application.Handlers.UserActivities;

public class GetAllUserActivitiesByDateTimeHandler : IRequestHandler<GetAllUserActivitiesByDateTimeQuery, List<DateTime>>
{
    private readonly IRepositoryManager _manager;

    public GetAllUserActivitiesByDateTimeHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<List<DateTime>> Handle(GetAllUserActivitiesByDateTimeQuery request, CancellationToken cancellationToken)
    {
       var allActivityDate =  await _manager.UserActivityHistoryRepository.GetByActivityTimeOfAllTime(request.UserId, cancellationToken);
        return allActivityDate;
    }
}
