using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    // kullanıcıya ders programı eklenebilir program haftalık olacak şekilde
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public UserProfile? UserSettings { get; set; }
    public ICollection<LessonSchedule>? LessonSchedule { get; set; }

    [Range(0,5)]
    public TargetExam TargetExam { get; set; } 
    public ICollection<UserActivityHistory> UserActivityHistories { get; set; } = new List<UserActivityHistory>();
    public ICollection<UserLessonProgress> LessonProgresses { get; set; } = new List<UserLessonProgress>();
    public ICollection<UserQuestionStat> QuestionStats { get; set; } = new List<UserQuestionStat>();
}
