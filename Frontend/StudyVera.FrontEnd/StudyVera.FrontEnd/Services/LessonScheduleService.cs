using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.LessonSchedules;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Utilities;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services;

public class LessonScheduleService : ILessonScheduleService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    private readonly string _apiUrl = $"{AppConsts.ApiBaseUrl}lesson-schedule";

    public LessonScheduleService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task Add(AddLessonScheduleDto dto)
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("Kullanıcı oturumu bulunamadı. Lütfen giriş yapın.");
        }

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var jsonContent = JsonContent.Create(dto);

            var response = await _httpClient.PostAsync(_apiUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Ders programı eklenirken bir hata oluştu. Durum Kodu: {response.StatusCode}. Hata: {errorContent}");
            }

            await _localStorage.RemoveItemAsync("lessonSchedules");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"HTTP isteği sırasında bir hata oluştu: {ex.Message}", ex);
        }
    }
    public async Task<List<LessonScheduleDto>> GetAll()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (string.IsNullOrEmpty(token))
            throw new Exception("Kullanıcı oturumu bulunamadı. Lütfen giriş yapın.");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var cachedSchedules = await _localStorage.GetItemAsync<List<LessonScheduleDto>>("lessonSchedules");

        if (cachedSchedules != null)
            return cachedSchedules;


        try
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var schedules = await response.Content.ReadFromJsonAsync<List<LessonScheduleDto>>();

                if (schedules != null)
                {
                    await _localStorage.SetItemAsync("lessonSchedules", schedules);
                    return schedules;
                }
            }

            Console.WriteLine($"API isteği başarısız oldu veya boş veri döndü. Durum Kodu: {response.StatusCode}");
            return new List<LessonScheduleDto>();

        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP isteği sırasında bir hata oluştu: {ex.Message}");
            return new List<LessonScheduleDto>();
        }
    }
}
