using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.UserProfile;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services;

public class UserProfileService : ServiceHelper, IUserProfileService
{
    private readonly string _apiUrl = $"{AppConsts.ApiBaseUrl}profile/user-profile";
    private readonly string _cacheKey = "user_profile_data";
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public UserProfileService(ILocalStorageService localStorage, HttpClient client)
        : base(localStorage, client)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<UserProfileDto?> GetProfileAsync()
    {
        var cachedProfile = await _localStorage.GetItemAsync<UserProfileDto>(_cacheKey);
        if (cachedProfile != null)
        {
            return cachedProfile;
        }

        await AddAuthorizationHeader();
        try
        {
            var response = await _client.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var profile = await response.Content.ReadFromJsonAsync<UserProfileDto>();

                if (profile != null)
                    await _localStorage.SetItemAsync(_cacheKey, profile);

                return profile;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task UpdateProfileAsync(UserProfileDto dto)
    {
        await AddAuthorizationHeader();
        var response = await _client.PatchAsJsonAsync(_apiUrl, dto);

        if (response.IsSuccessStatusCode)
        {
            await _localStorage.RemoveItemAsync(_cacheKey);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Profil güncelleme hatası: {error}");
        }
    }

    public async Task ClearCacheAsync()
    {
        await _localStorage.RemoveItemAsync(_cacheKey);
    }
}