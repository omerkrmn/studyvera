using Carter;
using MediatR;
using StudyVera.Application.Dtos.LessonSchedules;
using StudyVera.Application.Features.LessonSchedules.Commands;
using StudyVera.Application.Features.LessonSchedules.Queries;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class LessonScheduleModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/lesson-schedule")
                       .WithTags("Lesson Schedule")
                       .RequireAuthorization();

        group.MapGet("/", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            GetLessonScheduleQuery query = new();

            query.UserId = context.GetUserId();
            var response = await mediator.Send(query, ct);

            return response is not null
                ? Results.Ok(response)
                : Results.NotFound();
        })
        .Produces<LessonScheduleDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithName("GetLessonSchedule");

        group.MapPost("/", async (AddLessonScheduleCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();

            await mediator.Send(command, ct);

            return Results.Created();
        })
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithName("CreateLessonSchedule");
    }
}