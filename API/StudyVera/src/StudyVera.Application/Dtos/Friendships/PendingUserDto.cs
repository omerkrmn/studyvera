using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.Friendships;

public class PendingUserDto
{
    public int RequestId { get; init; }
    public string UserName { get; init; }

    public DateTime SentAt { get; init; }
}