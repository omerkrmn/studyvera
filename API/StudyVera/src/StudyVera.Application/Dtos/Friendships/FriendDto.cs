using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.Friendships;

public class FriendDto
{
    public string UserName { get; set; } = null!;
    public int Score { get; set; }
}
