using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.QuestionStatDetails.Queries;

namespace StudyVera.WebApi.Endpoints;

public class QuestionStatDetailModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/question-stat-details")
                       .WithTags("Question Stat Details")
                       .RequireAuthorization();

        group.MapGet("{questionStatId:int}", async (int questionStatId, ISender mediator, CancellationToken ct) =>
        {
            var query = new GetAllByUserQuestionStatQuery
            {
                QuestionStatId = questionStatId
            };

            var response = await mediator.Send(query, ct);

            return response is not null
                ? Results.Ok(response)
                : Results.NotFound();
        })
        .WithName("GetQuestionStatDetailsByQuestionStatId");
    }
}