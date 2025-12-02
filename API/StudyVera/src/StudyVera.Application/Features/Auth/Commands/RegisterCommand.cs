using MediatR;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudyVera.Application.Features.Auth.Commands;

public class RegisterCommand : IRequest<AppUser>
{
    // auth 
    [Required(ErrorMessage = "Firstname cannot be null!")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "LastName cannot be null!")]
    public string LastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public TargetExam TargetExam { get; set; }
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password cannot be null!")]
    public string Password { get; set; } = string.Empty;
}
