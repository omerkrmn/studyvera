using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Dtos;

namespace StudyVera.Application.Services;

public interface IAuthenticationManager
{
    Task<IdentityResult> RegisterUser(UserForRegistrationDto dto);
    Task<bool> ValidateUser(UserForAuthenticationDto dto);
    Task<TokenDto> CreateToken(bool populateExp);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
    Guid GetUserId();
}