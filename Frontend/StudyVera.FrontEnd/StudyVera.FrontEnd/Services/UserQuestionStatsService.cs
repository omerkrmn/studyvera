using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.UserLessonProgress;
using StudyVera.FrontEnd.Models.UserQuestionStat;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services;

public class UserQuestionStatsService : IUserQuestionStatsService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private string _baseUrl = $"{AppConsts.ApiBaseUrl}UserQuestion";

    public UserQuestionStatsService(HttpClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task Add(AddUserQuestionStatDto request)
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (string.IsNullOrEmpty(token))
            throw new Exception("Kullanıcı oturumu bulunamadı. Lütfen giriş yapın.");
        _client.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync(_baseUrl, request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Sunucu hatası: {response.StatusCode} → {error}");
        }
    }

    public async Task<List<UserQuestionStatDto>> GetAll()
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

        var solvedQuestions = JsonSerializer.Deserialize<List<UserQuestionStatDto>>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        return solvedQuestions ?? new List<UserQuestionStatDto>();
    }
    
}
