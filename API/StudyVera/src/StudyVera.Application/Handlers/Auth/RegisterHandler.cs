using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Domain.Entities; 
using StudyVera.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudyVera.Application.Handlers.Auth;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var user = request.Adapt<AppUser>();

        if (string.IsNullOrEmpty(user.UserName)) user.UserName = user.Email;

        user.InitializeDefaultSettingsAndStats();

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException(errors);
        }

        return true;
    }
}