using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Contract.Dtos;
using StudyVera.Contract.Interfaces;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Auth;

public class LoginHandler : IRequestHandler<LoginCommand, TokenDto>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var isValid = await _authenticationService.ValidateUser(request.UserForAuthenticationDto);

        // TODO: exceptionları düzenle
        if (!isValid)
            throw new UnauthorizedAccessException("Invalid username or password.");

        var token = await _authenticationService.CreateToken(populateExp: true);
        return token;
    }
}
