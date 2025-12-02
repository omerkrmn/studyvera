
namespace StudyVera.FrontEnd.Models.Common
{
    public class UserSettings
    {

        public string TargetExam { get; set; } = "KPSS";
        public int HaftalikHedefCalismaSuresi { get; set; } = 60;
        public string Email { get; set; } = "user@example.com";
        public string ColorHex { get; set; } = "#3498db";
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "light";
        public int DailyGoal { get; set; } = 30;
        public bool NotificationsEnabled { get; set; } = true;
        public bool IsPublicProfile { get; set; } = false;

    }
}
