using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => string.Join(" ", FirstName, LastName);

    public ExamTarget? TargetExam { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public ProfileStat? ProfileStat { get; set; }
    // kullanıcıya ders programı eklenebilir program haftalık olacak şekilde
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<UserActivityHistory?> UserActivityHistory { get; set; } = [];
    public ICollection<UserLessonProgress> LessonProgresses { get; set; } = [];
    public ICollection<UserQuestionStat> QuestionStats { get; set; } = [];
}
