using MediatR;
using StudyVera.Application.Dtos.UserProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserProfiles.Queries;

public class GetUserProfileQuery : IRequest<UserProfileDto>
{
    public Guid UserId { get; set; }
}
