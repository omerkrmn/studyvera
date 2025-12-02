using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.Topics;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services
{
    public class TopicService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly string _apiUrl = $"{AppConsts.ApiBaseUrl}topics";

        public TopicService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<List<TopicDto>> GetTopics(string? searchTerm = null)
        {
            try
            {
                var token = await _localStorage.GetItemAsync<string>("accessToken");
                if (string.IsNullOrEmpty(token))
                    throw new Exception("Kullanıcı oturumu bulunamadı. Lütfen giriş yapın.");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var url = string.IsNullOrWhiteSpace(searchTerm)
                    ? _apiUrl
                    : $"{_apiUrl}?searchTerm={Uri.EscapeDataString(searchTerm)}";

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var topics = await response.Content.ReadFromJsonAsync<List<TopicDto>>();
                    return topics ?? new List<TopicDto>();
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ API Error ({response.StatusCode}): {content}");
                return new List<TopicDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ TopicService.GetTopics error: {ex.Message}");
                return new List<TopicDto>();
            }
        }
    }
}
