using AutoMapper;
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
    private readonly IMapper _mapper;
    public RefreshTokenHandler(IAuthenticationService auth, IMapper mapper)
    {
        _auth = auth;
        _mapper = mapper;
    }

    public Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ParameterNullException("token dto cannot be null!");
        var result = _auth.RefreshToken(_mapper.Map<TokenDto>(request));
        return result;
    }
}
