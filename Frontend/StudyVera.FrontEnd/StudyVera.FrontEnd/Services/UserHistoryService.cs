using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.UserActivityHistories;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace StudyVera.FrontEnd.Services;

public class UserHistoryService : ServiceHelper , IUserHistoryService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private string _baseUrl = $"{AppConsts.ApiBaseUrl}user-activities";
    

    public UserHistoryService(HttpClient client, ILocalStorageService localStorage) : base(localStorage, client)
    {

        _client = client;
        _localStorage = localStorage;
    }

    public async Task<List<UserActivityHistoryDto>> GetAll()
    {

        await AddAuthorizationHeader();

        var response = await _client.GetAsync(_baseUrl);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Sunucu hatası ({response.StatusCode}): {content}");

        var progresses = JsonSerializer.Deserialize<List<UserActivityHistoryDto>>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        return progresses ?? new List<UserActivityHistoryDto>();

    }


    public async Task<List<DateTime>> GetAllTime()
    {
        await AddAuthorizationHeader();

        var response = await _client.GetAsync($"{_baseUrl}/get-all-by-date");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Sunucu hatası ({response.StatusCode}): {content}");

        var allDate = JsonSerializer.Deserialize<List<DateTime>>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ) ?? new List<DateTime>();

        var distinctDays = allDate
            .Select(d => d.Date)
            .OrderBy(d => d)
            .ToList();

        return distinctDays;
    }
}

