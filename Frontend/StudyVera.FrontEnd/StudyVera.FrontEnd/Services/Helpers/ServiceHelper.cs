using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace StudyVera.FrontEnd.Services.Helpers;

public class ServiceHelper
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _client = new HttpClient();

    public ServiceHelper(ILocalStorageService localStorage, HttpClient client)
    {
        _localStorage = localStorage;
        _client = client;
    }

    private async Task<string> GetAccessTokenAndCheck()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        if (string.IsNullOrEmpty(token))
            throw new Exception("Token bulunamadı, lütfen giriş yapın.");
        return token;
    }
    public async Task AddAuthorizationHeader()
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await GetAccessTokenAndCheck());
    }
}
