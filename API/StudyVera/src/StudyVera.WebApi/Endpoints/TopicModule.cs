using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.Topics.Queries;
using StudyVera.WebApi.Extensions; 

namespace StudyVera.WebApi.Endpoints;

public class TopicModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/topics")
                       .WithTags("Topics")
                       .RequireAuthorization();

        group.MapGet("/", async (string? searchTerm, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var targetExam = context.GetTargetExam();

            var query = new GetAllTopicsQuery
            {
                TargetExam = targetExam,
                SearchTerm = searchTerm
            };
            var response = await mediator.Send(query, ct);
            return Results.Ok(response);
        })
        .WithName("GetTopics");
    }
}