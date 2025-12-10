using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.UserLessonProgress;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace StudyVera.FrontEnd.Services;

public class UserLessonProgressService :ServiceHelper, IUserLessonProgressService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private string _baseUrl= $"{AppConsts.ApiBaseUrl}user-lesson-progresses";

    public UserLessonProgressService(HttpClient client, ILocalStorageService localStorage)
        : base(localStorage, client)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task Add(AddUserLessonProgressDto dto)
    {
        await AddAuthorizationHeader();

        var response = await _client.PostAsJsonAsync(_baseUrl, dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Sunucu hatası: {response.StatusCode} → {error}");
        }
    }


    public async Task<List<UserLessonProgressDto>> GetAll()
    {
        await AddAuthorizationHeader();

        var response = await _client.GetAsync(_baseUrl);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Sunucu hatası ({response.StatusCode}): {content}");

        var progresses = JsonSerializer.Deserialize<List<UserLessonProgressDto>>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        return progresses ?? new List<UserLessonProgressDto>();
    }
    public Task AddRange()
    {
        throw new NotImplementedException();
    }


    public async Task Update(int ulpId)
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        if (string.IsNullOrEmpty(token))
            throw new Exception("Kullanıcı oturumu bulunamadı. Lütfen giriş yapın.");

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        UpdateUserLessonProgressDto updateBody = new() { 
            ProgressStatus = Enums.ProgressStatus.Completed
        };

        var response = await _client.PutAsJsonAsync($"{_baseUrl}/{ulpId}", updateBody);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Güncelleme başarısız: {response.StatusCode} - {error}");
        }
    }

}
