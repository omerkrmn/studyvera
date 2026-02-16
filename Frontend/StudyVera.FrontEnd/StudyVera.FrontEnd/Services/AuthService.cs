using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using StudyVera.FrontEnd.Models.Auth;
using StudyVera.FrontEnd.Models.Common;
using StudyVera.FrontEnd.Providers;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services;

public class AuthService : ServiceHelper, IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly string _apiUrl = $"{AppConsts.ApiBaseUrl}auth";

    public AuthService(ILocalStorageService localStorage, HttpClient client, AuthenticationStateProvider authenticationStateProvider) : base(localStorage, client)
    {
        _httpClient = client;
        _localStorage = localStorage;
        _authStateProvider = authenticationStateProvider;
    }

    public async Task<bool> Login(LoginRequest loginRequest)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/login", loginRequest);
        if (!response.IsSuccessStatusCode)
            return false;
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (result == null) return false;
        await StoreTokens(result);
        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.AccessToken);
        return true;
    }

    public async Task<bool> Register(RegisterRequest registerRequest)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/register", registerRequest);
        if(response.IsSuccessStatusCode) return true;
        var errorData = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        throw new Exception(errorData?.Message ?? "Kayıt sırasında bilinmeyen bir hata oluştu.");
    }

    public async Task<string?> RefreshToken()
    {
        var accessToken = await _localStorage.GetItemAsync<string>("accessToken");
        var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

        var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/refresh",
            new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });

        if (!response.IsSuccessStatusCode)
        {
            await Logout();
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        if (result == null) return null;

        await StoreTokens(result);
        return result.AccessToken;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("accessToken");
        await _localStorage.RemoveItemAsync("refreshToken");

        ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> IsUserAuthenticated()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        return !string.IsNullOrEmpty(token);
    }

    private async Task StoreTokens(TokenResponse response)
    {
        await _localStorage.SetItemAsync("accessToken", response.AccessToken);
        await _localStorage.SetItemAsync("refreshToken", response.RefreshToken);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);
    }
}