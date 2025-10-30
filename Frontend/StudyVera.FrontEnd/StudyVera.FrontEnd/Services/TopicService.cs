using StudyVera.FrontEnd.Models.Topics;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services
{
    public class TopicService
    {
        private string apiUrl = "https://api.studyvera.tech/api/topic"; 

        private readonly HttpClient _httpClient;
        public TopicService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<TopicDto>> GetTopics()
        {
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var topics = await response.Content.ReadFromJsonAsync<List<TopicDto>>();
                return topics ?? new List<TopicDto>();
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Error Content: {content}");
            return new List<TopicDto>();
        }
    }
}
