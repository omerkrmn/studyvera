using MediatR;
using StudyVera.Contract.Dtos;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<AppUser>
    {
        public UserForRegistrationDto UserForRegistrationDto { get; set; }
    }

}
