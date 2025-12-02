using Mapster;
using MediatR;
using StudyVera.Application.Dtos;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Application.Services;
namespace StudyVera.Application.Handlers.Auth;

public class LoginHandler : IRequestHandler<LoginCommand, TokenDto>
{
    private readonly IAuthenticationManager _manager;

    public LoginHandler(IAuthenticationManager manager)
    {
        _manager = manager;
    }

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var isValid = await _manager.ValidateUser(request.Adapt<UserForAuthenticationDto>());

        if (!isValid)
            throw new UnauthorizedAccessException("Invalid username or password.");

        var token = await _manager.CreateToken(populateExp: true);
        return token;
    }
}
