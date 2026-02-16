using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities;

public class ProfileStat
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = null!;

    public int Score { get; set; }
    public int CurrentStreak { get; set; }
    public int BestStreak { get; set; }
    public DateTime? LastActivityDate { get; set; }
}
