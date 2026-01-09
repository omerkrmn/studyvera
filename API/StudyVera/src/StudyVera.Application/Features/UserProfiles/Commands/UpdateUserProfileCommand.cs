using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.UserProfiles.Commands;

public class UpdateUserProfileCommand : IRequest<bool>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    public int WeeklyQuestionGoal { get; set; }
    public int DailyStudyMinuteGoal { get; set; }

    public int DailyReminderHour { get; set; }
    public bool IsProfilePublic { get; set; }
    public bool ShowRankInLeaderboard { get; set; }
    public bool AllowFriendRequests { get; set; }

    public string Theme { get; set; } = "Dark";
    public string Language { get; set; } = "tr-TR";
}
