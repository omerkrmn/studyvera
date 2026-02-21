using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Friendships.Commands;

public class SendFriendRequestCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public string To { get; set; }
}
