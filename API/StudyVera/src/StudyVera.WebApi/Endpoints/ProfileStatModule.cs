using System.Text.Json;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Dtos.ProfileStats;
using StudyVera.Application.Features.ProfileStats.Queries;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class ProfileStatModule : ICarterModule
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/profile/stats")
                       .WithTags("Profile Stats")
                       .RequireAuthorization();

        group.MapGet("/me", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var query = new GetProfileStatByUserIdQuery
            {
                UserId = context.GetUserId()
            };

            var response = await mediator.Send(query, ct);

            return response is not null ? Results.Ok(response) : Results.NotFound();
        })
        .Produces<ProfileStatDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/", async ([AsParameters]GetScoreBoardQuery query, HttpResponse response, ISender mediator, CancellationToken ct) =>
        {

            var result = await mediator.Send(query, ct);

            var metaDataJson = JsonSerializer.Serialize(result.MetaData, _jsonOptions);
            response.Headers.Append("X-Pagination", metaDataJson);
            response.Headers.Append("Access-Control-Expose-Headers", "X-Pagination");

            return Results.Ok(result);
        });
    }
}