using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.Mock.UserMockExams.Commands;
using StudyVera.Application.Features.Mocks.UserMockExamDetails.Queries;
using StudyVera.Application.Features.Mocks.UserMockExams.Queries;
using StudyVera.WebApi.Extensions; 

namespace StudyVera.WebApi.Endpoints;

public class UserMockExamsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/mocks/")
                       .WithTags("User Exam Mock")
                       .RequireAuthorization();


        group.MapPost("/", async (CreateUserMockExamCommand command, ISender mediator, HttpContext context) =>
        {
            command.UserId = context.GetUserId();

            var result = await mediator.Send(command);
            return Results.Ok(result);
        });

        group.MapGet("/history", async (ISender mediator, HttpContext context) =>
        {
            var userId = context.GetUserId();
            var query = new GetUserMockExamHistoryQuery() { UserId = userId };

            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        group.MapGet("/{id:int}", async (int id, ISender mediator) =>
        {
            var query = new GetUserMockExamDetailQuery() { Id = id };

            var result = await mediator.Send(query);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        });
    }
}