using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.UserQuestionStats.Commands;
using StudyVera.Application.Features.UserQuestionStats.Queries;
using StudyVera.WebApi.Extensions; 

namespace StudyVera.WebApi.Endpoints;

public class UserQuestionModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/question-stats")
                       .WithTags("User Question Stats")
                       .RequireAuthorization(); 

        group.MapPost("/", async (AddUserQuestionStatCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            await mediator.Send(command, ct);

            return Results.Created();
        })
        .WithName("SolveQuestion");

        group.MapPost("/range", async (AddRangeUserQuestionStatCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            await mediator.Send(command, ct);

            return Results.Created();
        })
        .WithName("SolveQuestionRange");

        group.MapPost("/transfer", async (TransferUserQuestionStatCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            await mediator.Send(command, ct);

            return Results.Ok();
        })
        .WithName("TransferQuestionStat");

        group.MapGet("/", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            GetAllUserQuestionStatsByUserQuery query = new();
            query.UserId = context.GetUserId();
            var result = await mediator.Send(query, ct);

            return Results.Ok(result);
        })
        .WithName("GetUserQuestionStats");

        group.MapDelete("/detail/{id:int}", async (int id, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var command = new DeleteQuestionStatDetailCommand
            {
                UserId = context.GetUserId(),
                QuestionStatDetailId = id
            };
            await mediator.Send(command, ct);

            return Results.NoContent();
        })
        .WithName("DeleteQuestionStatDetail");

        group.MapPut("/detail/{id:int}", async (int id, UpdateQuestionStatDetailCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            command.QuestionStatDetailId = id;
            await mediator.Send(command, ct);

            return Results.NoContent();
        })
        .WithName("UpdateQuestionStatDetail");
    }
}