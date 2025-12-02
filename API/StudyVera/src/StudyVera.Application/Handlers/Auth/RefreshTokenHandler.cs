using Mapster;
using MediatR;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Dtos;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Application.Services;

namespace StudyVera.Application.Handlers.Auth;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
{
    private readonly IAuthenticationManager _auth;

    public RefreshTokenHandler(IAuthenticationManager service)
    {
        _auth = service;
    }

    public Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ParameterNullException("token dto cannot be null!");
        var result = _auth.RefreshToken(request.Adapt<TokenDto>());
        return result;
    }
}
