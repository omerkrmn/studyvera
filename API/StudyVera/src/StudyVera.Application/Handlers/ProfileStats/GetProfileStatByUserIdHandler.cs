using MediatR;
using StudyVera.Application.Dtos.ProfileStats;
using StudyVera.Application.Features.ProfileStats.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.ProfileStats;

public class GetProfileStatByUserIdHandler : IRequestHandler<GetProfileStatByUserIdQuery, ProfileStatDto>
{
    private readonly IRepositoryManager _manager;

    public GetProfileStatByUserIdHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<ProfileStatDto> Handle(GetProfileStatByUserIdQuery request, CancellationToken cancellationToken)
    {
        var score = await _manager.ProfileStatRepository.GetScoreByUserAsync(request.UserId, cancellationToken);
        return new ProfileStatDto() { Score = score };
    }
}
