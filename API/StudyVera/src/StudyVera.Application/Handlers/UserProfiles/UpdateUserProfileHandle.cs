using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserProfiles.Commands;
using StudyVera.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.UserProfiles;

public class UpdateUserProfileHandle : IRequestHandler<UpdateUserProfileCommand, bool>
{
    private readonly IRepositoryManager _manager;

    public UpdateUserProfileHandle(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _manager.UserProfileRepository
           .FindByCondition(up => up.UserId == request.UserId, trackChanges: true)
           .FirstOrDefaultAsync(cancellationToken);

        if (profile == null)
            throw new NotFoundException("Profil is not found.");

        request.Adapt(profile);

        profile.UpdatedAt = DateTime.UtcNow;

        await _manager.SaveChangesAsync();

        return true;
    }
}
