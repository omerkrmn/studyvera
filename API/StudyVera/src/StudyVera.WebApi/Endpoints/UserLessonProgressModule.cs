using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Application.Features.UserLessonProgresses.Queries;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class UserLessonProgressModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/user-lesson-progresses")
                       .WithTags("User Lesson Progress")
                       .RequireAuthorization();

        group.MapGet("/", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var query = new GetAllLessonProgressesQuery { UserId = context.GetUserId() };
            var response = await mediator.Send(query, ct);
            return Results.Ok(response);
        });

        group.MapPost("/", async (AddUserLessonProgressCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        });

        group.MapPost("add-range", async (AddRangeUserLessonProgressesCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            var result = await mediator.Send(command, ct);
            return Results.Ok(result);
        });

        group.MapPut("{ulpId:int}", async (int ulpId, UpdateUserLessonProgressCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            command.ulpId = ulpId;

            await mediator.Send(command, ct);
            return Results.NoContent();
        });
    }
}