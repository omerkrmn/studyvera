using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.Lessons;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services;

public class LessonService : ServiceHelper, ILessonService
{
    private readonly string _apiUrl = $"{AppConsts.ApiBaseUrl}lessons";
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public LessonService(ILocalStorageService localStorage, HttpClient client) : base(localStorage, client)
    {
        _localStorage = localStorage;
        _client = client;
    }

    public async Task<List<LessonDto>> GetAll()
    {
        await AddAuthorizationHeader();

        try
        {
            var response = await _client.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var lessons = await response.Content.ReadFromJsonAsync<List<LessonDto>>();
                if (lessons != null)
                {
                    return lessons;
                }
            }
            return new List<LessonDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("sikinti"); //TODO: değişecek
            return new List<LessonDto>();

        }


    }
}
