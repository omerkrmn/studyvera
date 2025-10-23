using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.Lessons.Commands;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public LessonControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson([FromBody] AddLessonCommand command, CancellationToken ct = default)
        {
            var lessonId = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(AddLesson), new { id = lessonId }, null);
        }
        [HttpPost("add-range")]
        public async Task<IActionResult> AddRangeLesson([FromBody] AddRangeLessonCommand command, CancellationToken ct = default)
        {
            var lessonIds = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(AddRangeLesson), lessonIds);
        }

    }
}
