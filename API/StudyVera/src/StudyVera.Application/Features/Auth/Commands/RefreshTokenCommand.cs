using MediatR;
using StudyVera.Application.Dtos;

namespace StudyVera.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public string AccessToken { get; init; }
        public string RefreshToken { get; init; }
    }

}
