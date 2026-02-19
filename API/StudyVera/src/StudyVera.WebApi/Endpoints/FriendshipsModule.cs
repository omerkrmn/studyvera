using Carter;
using MediatR;
using System.Security.Claims;

namespace StudyVera.WebApi.Endpoints;

public class FriendshipsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/friendship")
                               .WithTags("Friendships")
                               .RequireAuthorization();

        // 1. Onaylanmış Arkadaşları Getir
        group.MapGet("friends", async (ISender mediator, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new GetFriendsQuery(userId));
            return Results.Ok(result);
        });

        // 2. Bekleyen Arkadaşlık İsteklerini Getir (Gelenler)
        group.MapGet("requests/pending", async (ISender mediator, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new GetPendingRequestsQuery(userId));
            return Results.Ok(result);
        });

        // 3. Arkadaşlık İsteği At (Username ile)
        group.MapPost("request/{username}", async (string username, ISender mediator, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new CreateFriendRequestCommand(userId, username));
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        });

        // 4. İsteği Cevapla (Kabul/Red)
        group.MapPut("request/{requestId}/respond", async (Guid requestId, [FromBody] bool accept, ISender mediator, ClaimsPrincipal user) =>
        {
            var userId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await mediator.Send(new RespondFriendRequestCommand(requestId, userId, accept));
            return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
        });
    }
}
