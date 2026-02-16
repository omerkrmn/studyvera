using Blazored.LocalStorage;
using StudyVera.FrontEnd.Services.Concrats;
using System.Net.Http.Headers;

namespace StudyVera.FrontEnd.Handlers;
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly IAuthService _authService;

    public AuthHeaderHandler(ILocalStorageService localStorage, IAuthService authService)
    {
        _localStorage = localStorage;
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var newToken = await _authService.RefreshToken();

            if (!string.IsNullOrEmpty(newToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                response = await base.SendAsync(request, cancellationToken);
            }
            else
            {
                await _authService.Logout();
            }
        }

        return response;
    }
}