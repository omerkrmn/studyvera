﻿using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.Common;
using StudyVera.FrontEnd.Models.UserActivityHistories;
using StudyVera.FrontEnd.Models.UserWeeklyGoals;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Text.Json;

namespace StudyVera.FrontEnd.Services;

public class UserWeeklyGoalService : ServiceHelper, IUserWeeklyGoalService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;
    private string _baseUrl = $"{AppConsts.ApiBaseUrl}weekly-goal";
    public UserWeeklyGoalService(ILocalStorageService localStorage, HttpClient client) : base(localStorage, client)
    {
        _client = client;
        _localStorage = localStorage;
    }

    public async Task<UserWeeklyGoalDto> GetUserWeeklyGoal()
    {
        try
        {
            await AddAuthorizationHeader();

            var response = await _client.GetAsync($"{_baseUrl}/summary");

            if (!response.IsSuccessStatusCode)
            {
                await response.HandleError();
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserWeeklyGoalDto>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? new UserWeeklyGoalDto();
        }
        catch (HttpRequestException)
        {
            throw new Exception("İnternet bağlantınızı kontrol edin, sunucuya ulaşılamıyor.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
