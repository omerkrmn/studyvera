namespace StudyVera.FrontEnd.Models.UserProfile;
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
