using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Entities.Mock;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ProfileStat? ProfileStat { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public UserProfile? UserSettings { get; set; }
    public ICollection<LessonSchedule>? LessonSchedule { get; set; }

    [Range(0,5)]
    public TargetExam TargetExam { get; set; } 
    public ICollection<UserWeeklyGoal> UserWeeklyGoals { get; set; } = new List<UserWeeklyGoal>();
    public ICollection<UserActivityHistory> UserActivityHistories { get; set; } = new List<UserActivityHistory>();
    public ICollection<UserLessonProgress> LessonProgresses { get; set; } = new List<UserLessonProgress>();
    public ICollection<UserQuestionStat> QuestionStats { get; set; } = new List<UserQuestionStat>();

    public virtual ICollection<Friendship> SentFriendRequests { get; set; } = new List<Friendship>();
    public virtual ICollection<Friendship> ReceivedFriendRequests { get; set; } = new List<Friendship>();

    public ICollection<UserMockExam> MockExams { get; set; } = [];


    [NotMapped]
    public IEnumerable<AppUser> Friends =>
        SentFriendRequests.Where(f => f.Status == FriendshipStatus.Accepted).Select(f => f.Receiver)
        .Concat(ReceivedFriendRequests.Where(f => f.Status == FriendshipStatus.Accepted).Select(f => f.Requestor));

    public void InitializeDefaultSettingsAndStats()
    {
        Id = Guid.NewGuid();
        UserSettings = new UserProfile
        {
            UserId = Id,
            WeeklyQuestionGoal = 100,
            CurrentTitle = "Acemi",
            AllowFriendRequests = true,
            DailyReminderHour = 1,
            ShowRankInLeaderboard = true,
            DailyStudyMinuteGoal = 60,
            IsProfilePublic = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Language = "tr-TR",
            Theme = "Dark"
        };
        ProfileStat = new ProfileStat
        {
            UserId = Id,
            CurrentStreak = 0,
            BestStreak = 0,
            LastActivityDate = null
        };
    }
}
