using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Mock;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos;

public class AppUserDto
{
    public string UserName { get; set; }

    public ProfileStat? ProfileStat { get; set; }
    
    public UserProfile? UserSettings { get; set; }
    public ICollection<LessonSchedule>? LessonSchedule { get; set; }

    [Range(0, 5)]
    public TargetExam TargetExam { get; set; }
    public ICollection<UserWeeklyGoal> UserWeeklyGoals { get; set; } = new List<UserWeeklyGoal>();

}
