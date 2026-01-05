using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.Common;
using StudyVera.FrontEnd.Models.QuestionStatDetails;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services;

public class ProfileSummaryService : ServiceHelper, IProfileSummaryService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    private string _baseUrl = $"{AppConsts.ApiBaseUrl}profile/summary";

    public ProfileSummaryService(HttpClient client, ILocalStorageService localStorage) : base(localStorage, client)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<ProfileViewModel> GetProfileSummaryAsync()
    {
        await AddAuthorizationHeader();
        var response = await _client.GetAsync(_baseUrl);
        
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Sunucu hatası ({response.StatusCode}): {content}");

        var details = JsonSerializer.Deserialize<ProfileViewModel>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        return details ?? new ProfileViewModel();
    }
}
