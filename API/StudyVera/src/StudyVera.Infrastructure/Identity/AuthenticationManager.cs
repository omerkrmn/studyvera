using Google.Apis.Auth;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudyVera.Application.Dtos;
using StudyVera.Application.Services;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace StudyVera.Infrastructure.Identity;

public class AuthenticationManager(UserManager<AppUser> userManager, IOptions<JwtSettings> jwtOptions,IOptions<GoogleAuthSettings> googleAuthOptions) : IAuthenticationManager
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IOptions<JwtSettings> _jwtOptions = jwtOptions;
    private readonly GoogleAuthSettings _googleAuthSettings = googleAuthOptions.Value;


    private AppUser? _user;

    public async Task<TokenDto> CreateToken(bool populateExp)
    {
        var signinCredentials = GetSiginCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signinCredentials, claims);

        var refreshToken = GenerateRefreshToken();
        _user.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(_user);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new TokenDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        var user = await _userManager.FindByNameAsync(principal.Identity.Name);

        if (user is null ||
            user.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new Exception("Token error");

        _user = user;
        return await CreateToken(populateExp: true);
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistrationDto)
    {
        var user = userForRegistrationDto.Adapt<AppUser>();

        var result = await _userManager
            .CreateAsync(user, userForRegistrationDto.Password);

        if (result.Succeeded)
            await _userManager.AddToRolesAsync(user, new[] { "User" });
        return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
    {
        _user = await _userManager.FindByEmailAsync(userForAuthDto.Email);
        var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password));
        return result;
    }
    private SigningCredentials GetSiginCredentials()
    {
        var jwtSettings = _jwtOptions.Value;
        var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
            new Claim(ClaimTypes.Name, _user.UserName ?? string.Empty),
            new Claim("TargetExam", ((int)_user.TargetExam).ToString())
        };

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,
        List<Claim> claims)
    {
        var jwtSettings = _jwtOptions.Value;

        var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.Expires)),
                signingCredentials: signinCredentials);

        return tokenOptions;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _jwtOptions.Value;
        var secretKey = jwtSettings.SecretKey;

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken is null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token.");
        }
        return principal;
    }

    public Guid GetUserId()
        => _user!.Id;

    public async Task<TokenDto> GoogleSignIn(GoogleAuthDto googleAuth)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(googleAuth.IdToken, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _googleAuthSettings.ClientId }
        });

        var email = payload.Email;
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new AppUser
            {
                UserName = email.Split('@')[0],
                Email = email,
                FirstName = payload.GivenName ?? string.Empty,
                LastName = payload.FamilyName ?? string.Empty,
                EmailConfirmed = true,
                TargetExam = TargetExam.KPSS
            };

            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                throw new Exception($"Kullanıcı oluşturulamadı: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
            }

            await _userManager.AddToRoleAsync(user, "User");
        }

        _user = user;

        return await CreateToken(populateExp: true);
    }
}
