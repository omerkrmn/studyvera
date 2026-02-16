using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.ProfileStats;

public class ProfileStatDto
{
    public int Score { get; set; }
    public int CurrentStreak { get; set; }
    public int BestStreak { get; set; }
    public DateTime LastActivityDate { get; set; }
}
