using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.UserProfiles;

public class UserProfileDto
{
    public int WeeklyQuestionGoal { get; set; }
    public int DailyStudyMinuteGoal { get; set; }

    public int DailyReminderHour { get; set; }
    public bool IsProfilePublic { get; set; }
    public bool ShowRankInLeaderboard { get; set; }
    public bool AllowFriendRequests { get; set; }

    public string Theme { get; set; } = "Dark";
    public string Language { get; set; } = "tr-TR";

    public string CurrentTitle { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
