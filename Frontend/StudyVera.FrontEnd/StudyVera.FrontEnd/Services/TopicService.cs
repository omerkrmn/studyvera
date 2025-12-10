using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.Topics;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services
{
    public class TopicService : ServiceHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly string _apiUrl = $"{AppConsts.ApiBaseUrl}topics";

        public TopicService(HttpClient httpClient, ILocalStorageService localStorage) : base(localStorage, httpClient)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<List<TopicDto>> GetTopics(string? searchTerm = null)
        {
            try
            {
            await AddAuthorizationHeader();

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
                Console.WriteLine($"API Error ({response.StatusCode}): {content}");
                return new List<TopicDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TopicService.GetTopics error: {ex.Message}");
                return new List<TopicDto>();
            }
        }
    }
}
