using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Contract.Dtos;
using StudyVera.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<TokenDto>
{
    public UserForAuthenticationDto UserForAuthentication { get; set; }

}
