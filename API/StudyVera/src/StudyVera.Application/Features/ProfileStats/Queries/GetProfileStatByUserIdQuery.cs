using MediatR;
using StudyVera.Application.Dtos.ProfileStats;
using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.ProfileStats.Queries;

public class GetProfileStatByUserIdQuery : IRequest<ProfileStatDto>
{
    public Guid UserId { get; set; }
}
