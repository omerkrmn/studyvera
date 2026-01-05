using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.ProfileSummary;

public class ProfileViewModel
{
    public int TotalQuestions { get; set; }
    public int UserScore { get; set; }
    public int GlobalRank { get; set; }

    public int BadgesEarned { get; set; }
    public int CurrentStreak { get; set; }
    public int GoalRemainingQuestions { get; set; }
    public int GoalCompletionPercentage { get; set; }

    public Dictionary<DateTime, int> ActivityData { get; set; } = new();

    public Dictionary<string, int> DeficiencyTopics { get; set; } = new();

}
