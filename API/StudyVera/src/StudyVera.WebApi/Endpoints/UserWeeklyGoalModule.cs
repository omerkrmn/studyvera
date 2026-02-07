using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.UserWeeklyGoal.Commands;
using StudyVera.Application.Features.UserWeeklyGoal.Queries;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class UserWeeklyGoalModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/weekly-goal")
                       .WithTags("Weekly Goals")
                       .RequireAuthorization();

        group.MapGet("summary", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            GetWeeklyGoalQuery query = new();
            query.UserId = context.GetUserId();
            var result = await mediator.Send(query, ct);

            return Results.Ok(result);
        })
        .WithName("GetCurrentWeeklySummary");

        group.MapPost("update-progress", async (CreateQuestionSolveCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            var result = await mediator.Send(command, ct);

            return Results.Ok(result);
        })
        .WithName("UpdateWeeklyProgress");
    }
}