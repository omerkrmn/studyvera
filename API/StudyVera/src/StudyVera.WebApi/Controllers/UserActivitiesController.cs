using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.UserActivities.Queries;
using System.Security.Claims;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/user-activities")]
    [ApiController]
    public class UserActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");

            GetAllUserActivityHistoryByUserQuery query = new()
            {
                UserId = Guid.Parse(userIdClaim)
            };
            var response = await _mediator.Send(query, ct);
            return Ok(response);
        }
        [Authorize]
        [HttpGet("get-all-by-date")]
        public async Task<IActionResult> GetAllDateTime(CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");
            GetAllUserActivitiesByDateTimeQuery query = new()
            {
                UserId = Guid.Parse(userIdClaim)
            };
            var response = await _mediator.Send(query, ct);
            return Ok(response);
        }

    }
}
