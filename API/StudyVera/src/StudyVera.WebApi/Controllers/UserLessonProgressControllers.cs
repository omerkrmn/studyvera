using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Common.Exceptions;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Application.Features.UserLessonProgresses.Queries;
using System.Security.Claims;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/user-lesson-progresses")]
    [ApiController]
    public class UserLessonProgressControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserLessonProgressControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");
            GetAllLessonProgressesQuery getAllLessonProgressesQuery1 = new()
            {
                UserId = new Guid(userIdClaim)
            };

            var response = await _mediator.Send(getAllLessonProgressesQuery1, ct);
            return Ok(response);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProgress([FromBody] AddUserLessonProgressCommand command, CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");

            command.UserId = new Guid(userIdClaim);

            if (command is null)
                throw new ParameterNullException("Request body cannot be null!");

            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("add-range")]
        public async Task<IActionResult> AddRange([FromBody] AddRangeUserLessonProgressesCommand command, CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is null)
                return Unauthorized("UserId claim not found in token");

            command.UserId = Guid.Parse(userIdClaim);

            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("{topicId}")]
        public async Task<IActionResult> Update([FromRoute] int topicId, [FromBody] UpdateUserLessonProgressCommand command, CancellationToken ct)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim is null)
                return Unauthorized("UserId claim not found in token");

            command.UserId = Guid.Parse(userIdClaim);

            command.TopicId = topicId;

            await _mediator.Send(command, ct);
            return NoContent();
        }

    }
}
