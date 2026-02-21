using MediatR;
using StudyVera.Application.Dtos.Friendships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Friendships.Queries;

public class  GetSentRequestsQuery : IRequest<List<PendingUserDto>>
{
    public Guid UserId { get; set; }
}