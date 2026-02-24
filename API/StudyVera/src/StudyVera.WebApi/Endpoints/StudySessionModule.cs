using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.StudySessions.Commands;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class StudySessionModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/study-sessions")
                       .WithTags("Study Sessions")
                       .RequireAuthorization();

        group.MapPost("/", async (CreateStudySessionCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var commandWithUser = command with { UserId = context.GetUserId() };
            var result = await mediator.Send(commandWithUser, ct);
            return Results.Ok(result);
        })
        .WithName("CreateStudySession");
    }
}