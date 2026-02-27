using Mapster;
using MediatR;
using StudyVera.Application.Dtos;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Application.Services;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
namespace StudyVera.Application.Handlers.Auth;

public class GoogleAuthHandler : IRequestHandler<GoogleAuthCommand, TokenDto>
{
    private readonly IAuthenticationManager _manager;

    public GoogleAuthHandler(IAuthenticationManager manager)
    {
        _manager = manager;
    }

    public async Task<TokenDto> Handle(GoogleAuthCommand request, CancellationToken cancellationToken)
    {
        GoogleAuthDto googleAuthDto = request.Adapt<GoogleAuthDto>();
        var token = await _manager.GoogleSignIn(googleAuthDto);
        return token;
    }
}