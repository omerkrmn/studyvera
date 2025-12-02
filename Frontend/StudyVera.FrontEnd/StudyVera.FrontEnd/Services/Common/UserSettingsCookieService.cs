using Blazored.LocalStorage;
using Microsoft.AspNetCore.Http;
using StudyVera.FrontEnd.Models.Common;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services.Common;


public class UserSettingsService
{
    private readonly ILocalStorageService _localStorage;

    private const string Key = "UserSettings";

    public UserSettingsService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<UserSettings> Load()
    {
        var settings = await _localStorage.GetItemAsync<UserSettings>(Key);

        if (settings == null)
            return new UserSettings(); 

        return settings;
    }

    public async Task Save(UserSettings settings)
    {
        await _localStorage.SetItemAsync(Key, settings);
    }
}