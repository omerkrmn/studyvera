using Carter;
using MediatR;
using StudyVera.Application.Features.Lessons.Commands;
using StudyVera.Application.Features.Lessons.Queries;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class LessonModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/lessons")
                       .WithTags("Lessons");

        group.MapGet("/", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            GetLessonsQuery query = new();
            query.TargetExam = context.GetTargetExam();
            var lessons = await mediator.Send(query, ct);
            return Results.Ok(lessons);
        })
        .RequireAuthorization();

        group.MapPost("/", async (AddLessonCommand command, ISender mediator, CancellationToken ct) =>
        {
            var lessonId = await mediator.Send(command, ct);
            return Results.Created($"/api/lessons/{lessonId}", new { id = lessonId });
        });

        group.MapPost("add-range", async (AddRangeLessonCommand command, ISender mediator, CancellationToken ct) =>
        {
            var lessonIds = await mediator.Send(command, ct);
            return Results.Created($"/api/lessons/range", lessonIds);
        });
    }
}