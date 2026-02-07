using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.ProfileSummary.Queries;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class ProfileSummaryModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/profile")
                       .WithTags("Profile")
                       .RequireAuthorization();

        group.MapGet("summary", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            GetProfileSummaryQuery query = new();

            query.UserId = context.GetUserId();
            query.TargetExam = context.GetTargetExam();

            var response = await mediator.Send(query, ct);

            return response is not null ? Results.Ok(response) : Results.NotFound();
        })
        .WithName("GetProfileSummary");
    }
}