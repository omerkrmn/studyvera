using StudyVera.FrontEnd.Models.Common;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IProfileSummaryService
{
    Task<ProfileViewModel> GetProfileSummaryAsync();
}
