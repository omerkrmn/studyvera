using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using StudyVera.Application.Features.Friendships.Commands;
using StudyVera.Application.Features.Friendships.Queries;
using StudyVera.WebApi.Extensions;
using System.Security.Claims;

namespace StudyVera.WebApi.Endpoints;

public class FriendshipsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/friendship")
                               .WithTags("Friendships")
                               .RequireAuthorization();
        //arkadaşları getir.
        group.MapGet("/", async (HttpContext context, ISender mediator, CancellationToken ct) =>
        {
            var userId = context.GetUserId();
            var query = new GetFriendScoresQuery { UserId = userId };
            var response = await mediator.Send(query, ct);
            return Results.Ok(response);
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound)
        .WithName("GetAllFriendships");

        group.MapPost("/request/{friendUserName}", async (HttpContext context, ISender mediator, string friendUserName, CancellationToken ct) =>
        {
            var userId = context.GetUserId();
            var command = new SendFriendRequestCommand { UserId = userId, To = friendUserName };
            var response = await mediator.Send(command, ct);

            return Results.Ok(response);
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithName("SendFriendRequest");



        group.MapPost("/accept/{userName}", async (HttpContext context, ISender mediator, string userName, CancellationToken ct) =>
        {
            var userId = context.GetUserId();
            var command = new AcceptFriendRequestCommand { UserId = userId, From = userName };
            var response = await mediator.Send(command, ct);

            return Results.Ok(response);
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithName("AcceptFriendRequest");

        group.MapGet("/requests", async (HttpContext context, ISender mediator) =>
        {
            var userId = context.GetUserId();
            var response = await mediator.Send(new GetReceivedRequestsQuery { UserId = userId });
            return Results.Ok(response);
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .WithName("GetReceivedRequests");
    }
}
