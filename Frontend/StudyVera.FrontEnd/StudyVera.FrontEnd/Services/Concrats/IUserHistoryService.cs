using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.UserActivityHistories;
using StudyVera.FrontEnd.Models.UserLessonProgress;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IUserHistoryService
{
    Task<List<UserActivityHistoryDto>> GetAll();

    Task<List<DateTime>> GetAllTime();
}
