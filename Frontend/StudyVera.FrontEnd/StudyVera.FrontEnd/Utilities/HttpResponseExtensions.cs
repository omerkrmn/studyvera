using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using StudyVera.FrontEnd.Models.Common;

namespace StudyVera.FrontEnd.Utilities
{
    public static class HttpResponseExtensions
    {
        public static async Task HandleError(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                var errorData = JsonSerializer.Deserialize<ErrorResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                throw new Exception(errorData?.Message ?? "Bir hata oluştu.");
            }
            catch (JsonException)
            {
                throw new Exception(content);
            }
        }
    }
}