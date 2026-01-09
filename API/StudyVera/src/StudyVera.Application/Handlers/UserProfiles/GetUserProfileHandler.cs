using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Dtos.UserProfiles;
using StudyVera.Application.Features.UserProfiles.Queries;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserProfiles;

public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IRepositoryManager _manager;

    public GetUserProfileHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userProfileDto = await _manager.UserProfileRepository
                            .FindByCondition(up => up.UserId == request.UserId, trackChanges: false)
                            .ProjectToType<UserProfileDto>()
                            .FirstOrDefaultAsync(cancellationToken);

        if (userProfileDto == null)
            throw new NotFoundException("Profil bulunamadı.");

        return userProfileDto;
    }
}
