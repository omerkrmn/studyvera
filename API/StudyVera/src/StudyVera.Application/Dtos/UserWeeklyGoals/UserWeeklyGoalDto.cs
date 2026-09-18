using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.UserWeeklyGoals;

public class UserWeeklyGoalDto
{
    public DateTime WeekStartDate { get; set; }

    public int TargetQuestionCount { get; set; }
    public int TargetStudyMinutes { get; set; }

    public int CurrentQuestionCount { get; set; }
    public int CurrentStudyMinutes { get; set; }


    public int RemainingQuestions => Math.Max(0, TargetQuestionCount - CurrentQuestionCount);
    public int RemainingStudyMinutes => Math.Max(0, TargetStudyMinutes - CurrentStudyMinutes);

    public double CompletionPercentage
    {
        get
        {
            if (TargetQuestionCount <= 0) return 0;
            var percent = (double)CurrentQuestionCount / TargetQuestionCount * 100;
            return Math.Min(100, Math.Round(percent, 1));
        }
    }

    public double StudyCompletionPercentage
    {
        get
        {
            if (TargetStudyMinutes <= 0) return 0;
            var percent = (double)CurrentStudyMinutes / TargetStudyMinutes * 100;
            return Math.Min(100, Math.Round(percent, 1));
        }
    }

    public bool IsGoalAchieved => CurrentQuestionCount >= TargetQuestionCount;
    public bool IsStudyGoalAchieved => CurrentStudyMinutes >= TargetStudyMinutes;

    public string StatusMessage => IsGoalAchieved
        ? "Tebrikler, haftalık hedefine ulaştın!"
        : $"{RemainingQuestions} soru sonra hedefine ulaşacaksın.";
}
