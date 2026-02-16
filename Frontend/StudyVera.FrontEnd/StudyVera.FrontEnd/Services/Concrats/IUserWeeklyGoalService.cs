using StudyVera.FrontEnd.Models.UserWeeklyGoals;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IUserWeeklyGoalService
{
    public Task<UserWeeklyGoalDto> GetUserWeeklyGoal();
}
