using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.UserActivities.Queries;
using StudyVera.WebApi.Extensions; 

namespace StudyVera.WebApi.Endpoints;

public class UserActivitiesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/user-activities")
                       .WithTags("User Activities")
                       .RequireAuthorization();

        group.MapGet("/", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var query = new GetAllUserActivityHistoryByUserQuery
            {
                UserId = context.GetUserId()
            };

            var response = await mediator.Send(query, ct);
            return Results.Ok(response);
        })
        .WithName("GetAllUserActivities");

        group.MapGet("get-all-by-date", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var query = new GetAllUserActivitiesByDateTimeQuery
            {
                UserId = context.GetUserId()
            };

            var response = await mediator.Send(query, ct);
            return Results.Ok(response);
        })
        .WithName("GetAllUserActivitiesByDate");
    }
}