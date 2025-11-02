using AutoMapper;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Handlers.Auth
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, AppUser>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ParameterNullException("parameters cannot be null!");
            var user = request.UserForRegistrationDto.Adapt<AppUser>();
            var result = await _userManager.CreateAsync(user, request.UserForRegistrationDto.Password);

            if (result.Succeeded)
                return user;
            throw new Exception("User registration failed!");
        }
    }

}
