﻿using Blazored.LocalStorage;
using StudyVera.FrontEnd.Models.Mocks;
using StudyVera.FrontEnd.Services.Concrats;
using StudyVera.FrontEnd.Services.Helpers;
using StudyVera.FrontEnd.Utilities;
using System.Net.Http.Json;

namespace StudyVera.FrontEnd.Services;

public class UserMockExamService : ServiceHelper, IUserMockExamService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = $"{AppConsts.ApiBaseUrl}mocks/";

    public UserMockExamService(ILocalStorageService localStorage, HttpClient client) : base(localStorage, client)
    {
        _httpClient = client;
    }

    public async Task<int> CreateMockExamAsync(CreateUserMockExamCommand request)
    {
        try
        {
            await AddAuthorizationHeader();

            var response = await _httpClient.PostAsJsonAsync(_baseUrl, request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<int>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CreateMockExamAsync Error: {ex.Message}");
        }

        return 0;
    }

    public async Task<UserMockExamDetailResponse?> GetMockExamDetailAsync(int id)
    {
        try
        {
            await AddAuthorizationHeader();

            return await _httpClient.GetFromJsonAsync<UserMockExamDetailResponse>($"{_baseUrl}{id}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<UserMockExamResponse>> GetUserMockExamsAsync()
    {
        try
        {
            await AddAuthorizationHeader();

            var response = await _httpClient.GetFromJsonAsync<List<UserMockExamResponse>>($"{_baseUrl}history");

            return response ?? new List<UserMockExamResponse>();
        }
        catch
        {
            return new List<UserMockExamResponse>();
        }
    }
}
