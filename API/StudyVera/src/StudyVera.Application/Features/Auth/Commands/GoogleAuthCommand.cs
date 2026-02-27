using MediatR;
using StudyVera.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace StudyVera.Application.Features.Auth.Commands;

public class GoogleAuthCommand : IRequest<TokenDto>
{
    public string IdToken { get; set; }
}