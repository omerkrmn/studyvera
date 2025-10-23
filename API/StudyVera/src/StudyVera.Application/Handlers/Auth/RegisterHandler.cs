using AutoMapper;
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
        private readonly IMapper _mapper;

        public RegisterHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AppUser> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ParameterNullException("parameters cannot be null!");
            var user = _mapper.Map<AppUser>(request.UserForRegistration);
            var result = await _userManager.CreateAsync(user, request.UserForRegistration.Password);

            if (result.Succeeded)
                return user;
            throw new Exception("User registration failed!");
        }
    }

}
