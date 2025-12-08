using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserQuestionStats.Commands;
using StudyVera.Application.Features.UserQuestionStats.Queries;
using System.Security.Claims;
using System.Text.Json;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/question-stats")]
    [ApiController]
    public class UserQuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserQuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SolveQuestion([FromBody] AddUserQuestionStatCommand command, CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();

            command.UserId = Guid.Parse(userId);

            await _mediator.Send(command, ct);

            return Ok("İstatistik güncellendi");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult>
            Get([FromQuery] GetAllUserQuestionStatsByUserQuery query)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();


            query.UserId = Guid.Parse(userId);

            var result = await _mediator.Send(query);

            var metaDataJson = JsonSerializer.Serialize(result.MetaData, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Response.Headers.Append("X-Pagination", metaDataJson);

            return Ok(result);
        }
    }
}
