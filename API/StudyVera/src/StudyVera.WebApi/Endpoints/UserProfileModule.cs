using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using StudyVera.Application.Features.UserProfiles.Commands;
using StudyVera.Application.Features.UserProfiles.Queries;
using StudyVera.WebApi.Extensions;

namespace StudyVera.WebApi.Endpoints;

public class UserProfileModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/profile/user-profile")
                       .WithTags("User Profile")
                       .RequireAuthorization();

        group.MapGet("/", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var query = new GetUserProfileQuery
            {
                UserId = context.GetUserId()
            };

            var response = await mediator.Send(query, ct);
            return Results.Ok(response);
        });

        group.MapPatch("/", async (UpdateUserProfileCommand command, HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            command.UserId = context.GetUserId();
            var result = await mediator.Send(command, ct);
            return result
                ? Results.NoContent()
                : Results.BadRequest("Profile could not be updated.");
        });
    }
}