using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.LessonSchedules.Commands;
using StudyVera.Application.Features.LessonSchedules.Queries;
using System.Security.Claims;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/lesson-schedule")]
    [ApiController]
    public class LessonScheduleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery]GetLessonScheduleQuery query)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");
            query.UserId = Guid.Parse(userIdClaim);
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddLessonScheduleCommand query)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");

            query.UserId = Guid.Parse(userIdClaim);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(query);

            return Created();
        }
    }
}
