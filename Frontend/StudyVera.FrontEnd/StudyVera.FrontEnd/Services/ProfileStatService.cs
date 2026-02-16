using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.LessonSchedules;
using StudyVera.FrontEnd.Models.ProfileStats;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services;

public class ProfileStatService : ServiceHelper, IProfileStatService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = $"{AppConsts.ApiBaseUrl}profile/stats/";
    public ProfileStatService(ILocalStorageService localStorage, HttpClient client) : base(localStorage, client)
    {
        _httpClient = client;
    }

    public async Task<ProfileStatDto> GetScoreAsync()
    {
        await AddAuthorizationHeader();
        try
        {
            var response = await _httpClient.GetAsync(_apiUrl + "me");
        }
        catch (Exception)
        {

            throw;
        }
        throw new NotImplementedException();
    }

    public async Task<List<ScoreBoardDto>> GetScoreBoardAsync()
    {
        await AddAuthorizationHeader();
        try
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}?PageNumber=1&PageSize=5");
            if (response.IsSuccessStatusCode)
            {
                var scoreBoardResult = await response.Content.ReadFromJsonAsync<List<ScoreBoardDto>>();
                if (scoreBoardResult != null)
                    return scoreBoardResult;

            }
        }
        catch (Exception)
        {
            Console.WriteLine("Veri alınırken hata oluştu!. lütfen daha sonra tekrar deneyiniz.");
            return new List<ScoreBoardDto>();
        }
        return new List<ScoreBoardDto>();
    }
}
