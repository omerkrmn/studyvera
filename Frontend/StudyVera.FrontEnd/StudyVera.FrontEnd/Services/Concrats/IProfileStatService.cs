using StudyVera.FrontEnd.Models.ProfileStats;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IProfileStatService
{
    public Task<List<ScoreBoardDto>> GetScoreBoardAsync();
    public Task<ProfileStatDto> GetScoreAsync();
}
