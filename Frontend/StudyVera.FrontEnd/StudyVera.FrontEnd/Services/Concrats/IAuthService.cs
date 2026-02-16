using StudyVera.FrontEnd.Models.Auth;

namespace StudyVera.FrontEnd.Services.Concrats;

public interface IAuthService
{
    Task<bool> Login(LoginRequest loginRequest);
    Task<bool> Register(RegisterRequest registerRequest);
    Task<string?> RefreshToken();
    Task Logout();
    Task<bool> IsUserAuthenticated();
}