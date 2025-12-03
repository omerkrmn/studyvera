using Mapster;
using MediatR;
using StudyVera.Application.Dtos;
using StudyVera.Application.Features.Auth.Commands;
using StudyVera.Application.Services;
using StudyVera.Domain.Enums;
using StudyVera.Domain.Interfaces;
namespace StudyVera.Application.Handlers.Auth;

public class LoginHandler : IRequestHandler<LoginCommand, TokenDto>
{
    private readonly IAuthenticationManager _manager;
    private readonly IRepositoryManager _repository;


    public LoginHandler(IAuthenticationManager manager, IRepositoryManager repository)
    {
        _manager = manager;
        _repository = repository;
    }

    public async Task<TokenDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var isValid = await _manager.ValidateUser(request.Adapt<UserForAuthenticationDto>());

        if (!isValid)
            throw new UnauthorizedAccessException("Invalid username or password.");

        var token = await _manager.CreateToken(populateExp: true);

        _repository.UserActivityHistoryRepository.Create(
            new()
            {
                UserId= _manager.GetUserId(),   
                ActivityType = ActivityType.UserLogins,
                Description = "Kullanıcı sisteme giriş yaptı",
                ActivityDate = DateTime.UtcNow,
            });

        await _repository.SaveChangesAsync();
        return token;
    }
}
