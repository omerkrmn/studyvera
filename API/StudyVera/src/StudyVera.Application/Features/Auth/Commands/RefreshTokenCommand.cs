using MediatR;
using StudyVera.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public TokenDto TokenDto { get; set; }  
    }

}
