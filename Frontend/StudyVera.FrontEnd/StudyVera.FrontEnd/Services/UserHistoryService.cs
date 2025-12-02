using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.UserActivityHistories;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services;

public class UserHistoryService : IUserHistoryService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private string _baseUrl = $"{AppConsts.ApiBaseUrl}user-activities";

    public UserHistoryService(HttpClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<List<UserActivityHistoryDto>> GetAll()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token bulunamadı, lütfen giriş yapın.");

        _client.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue("Bearer", token);

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
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token bulunamadı, lütfen giriş yapın.");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

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
