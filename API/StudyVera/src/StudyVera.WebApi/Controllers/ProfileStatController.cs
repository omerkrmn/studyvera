using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Dtos.ProfileStats;
using StudyVera.Application.Features.ProfileStats.Queries;
using StudyVera.WebApi.Controllers.Common;
using System.Text.Json;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/profile/stats/")]
    [ApiController]
    public class ProfileStatController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileStatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("me")]
        [Authorize]
        [ProducesResponseType(typeof(ProfileStatDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProfileStatDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfileStatByUserId(CancellationToken ct)
        {
            var query = new GetProfileStatByUserIdQuery { UserId = CurrentUserId };
            var response = await _mediator.Send(query, ct);
            return response != null ? Ok(response) : NotFound();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetScoreBoard(CancellationToken ct)
        {
            var query = new GetScoreBoardQuery();
            var result =  await _mediator.Send(query, ct);

            var metaDataJson = JsonSerializer.Serialize(result.MetaData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Response.Headers.Append("X-Pagination", metaDataJson);

            return Ok(result);
        }

    }
}
