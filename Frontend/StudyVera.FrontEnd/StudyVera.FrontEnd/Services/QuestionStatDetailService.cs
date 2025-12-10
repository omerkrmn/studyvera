using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.QuestionStatDetails;
using StudyVera.FrontEnd.Models.UserQuestionStat;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services;

public class QuestionStatDetailService :ServiceHelper, IQuestionStatDetailService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    private string _baseUrl = $"{AppConsts.ApiBaseUrl}question-stat-details/";

    public QuestionStatDetailService(HttpClient client, ILocalStorageService localStorage) : base(localStorage, client)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<List<QuestionStatDetailDto>> GetAll(int questionStatId)
    {
        await AddAuthorizationHeader();


        var response = await _client.GetAsync($"{_baseUrl}{questionStatId}");
        
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Sunucu hatası ({response.StatusCode}): {content}");

        var details = JsonSerializer.Deserialize<List<QuestionStatDetailDto>>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
        return details ?? new List<QuestionStatDetailDto>();
    }
}
