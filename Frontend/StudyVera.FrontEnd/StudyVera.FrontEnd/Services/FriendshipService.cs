using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.Common;
using StudyVera.FrontEnd.Models.Friendships;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Reflection.Metadata;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services;

public class FriendshipService : ServiceHelper, IFriendshipService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl = $"{AppConsts.ApiBaseUrl}friendship/";

    public FriendshipService(ILocalStorageService localStorage, HttpClient client) : base(localStorage, client)
    {
        _client = client;
    }

    public async Task AddFriend(string friendUserName)
    {
        await AddAuthorizationHeader();
        var response = await _client.PostAsync($"{_baseUrl}request/{friendUserName}", null);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content); 
        }
    }

    public async Task AcceptFriend(string friendUserName)
    {
        await AddAuthorizationHeader();
        var response = await _client.PostAsync($"{_baseUrl}accept/{friendUserName}", null);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<List<FriendDto>> GetAllFriends()
    {
        await AddAuthorizationHeader();
        var response = await _client.GetAsync(_baseUrl);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        var contentStream = await response.Content.ReadAsStreamAsync();
        var friends = await JsonSerializer.DeserializeAsync<List<FriendDto>>(
            contentStream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        return friends ?? new List<FriendDto>();
    }

    public async Task<List<PendingUserDto>> GetReceivedRequests()
    {
        await AddAuthorizationHeader();

        var response = await _client.GetAsync($"{_baseUrl}requests");

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception($"İstekler getirilemedi: {content}");
        }

        var contentStream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<List<PendingUserDto>>(
            contentStream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        return result ?? new List<PendingUserDto>();
    }
}