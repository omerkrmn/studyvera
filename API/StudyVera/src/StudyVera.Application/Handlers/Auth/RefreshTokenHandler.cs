using AutoMapper;
using Mapster;
using MediatR;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Contract.Dtos;
using StudyVera.Contract.Interfaces;
using StudyVera.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Auth;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
{
    private readonly IAuthenticationService _auth;
    public RefreshTokenHandler(IAuthenticationService auth)
    {
        _auth = auth;
    }

    public Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ParameterNullException("token dto cannot be null!");
        var result = _auth.RefreshToken(request.Adapt<TokenDto>());
        return result;
    }
}
