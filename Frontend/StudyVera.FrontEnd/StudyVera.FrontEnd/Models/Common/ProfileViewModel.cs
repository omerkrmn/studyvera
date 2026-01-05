namespace StudyVera.FrontEnd.Models.Common;

public class ProfileViewModel
{
    public string UserName { get; set; } = "StudyVeraKullanicisi";
    public string Email { get; set; } = "user@studyvera.com";
    public string TargetExam { get; set; } = "TYT & AYT";
    public int TotalQuestions { get; set; } = 78945;
    public int UserScore { get; set; } = 2105;
    public int GlobalRank { get; set; } = 123;

    public int BadgesEarned { get; set; } = 8;
    public int CurrentStreak { get; set; } = 35;
    public int GoalRemainingQuestions { get; set; } = 250;
    public int GoalCompletionPercentage { get; set; } = 75;

    public Dictionary<DateTime, int> ActivityData { get; set; } = new();

    public Dictionary<string, int> DeficiencyTopics { get; set; } = new();

}
