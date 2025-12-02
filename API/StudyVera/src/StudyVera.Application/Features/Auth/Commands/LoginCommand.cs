using MediatR;
using StudyVera.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace StudyVera.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<TokenDto>
{
    [Required(ErrorMessage = "E-Mail address cannot be null!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password cannot be null!")]
    public string Password { get; set; }

}
