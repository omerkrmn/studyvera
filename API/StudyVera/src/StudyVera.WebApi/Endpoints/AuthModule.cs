using Carter;
using MediatR;
using StudyVera.Application.Features.Auth.Commands;

namespace StudyVera.WebApi.Endpoints;

public class AuthModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/auth")
                       .WithTags("Auth");

        group.MapPost("login", async (LoginCommand command, ISender mediator) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        });

        group.MapPost("register", async (RegisterCommand command, ISender mediator) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        });

        group.MapPost("refresh", async (RefreshTokenCommand command, ISender mediator) =>
        {
            var response = await mediator.Send(command);
            return Results.Ok(response);
        });
    }
}