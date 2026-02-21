using MediatR;
using StudyVera.Application.Dtos.Friendships;
using StudyVera.Application.Features.Friendships.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Friendships;

public class GetReceivedRequestsHandler : IRequestHandler<GetReceivedRequestsQuery, IEnumerable<PendingUserDto>>
{
    private readonly IRepositoryManager _manager;

    public GetReceivedRequestsHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IEnumerable<PendingUserDto>> Handle(GetReceivedRequestsQuery request, CancellationToken cancellationToken)
    {
        var pendingRequests = await _manager.FriendshipRepository.GetPendingRequestsAsync(request.UserId, cancellationToken);

        if (pendingRequests == null || !pendingRequests.Any())
            return Enumerable.Empty<PendingUserDto>();
        

        var response = pendingRequests.Select(f => new PendingUserDto
        {
            RequestId = f.Id,
            UserName = f.Requestor?.UserName,
            SentAt = f.CreatedDate,
        });

        return response;
    }
}
