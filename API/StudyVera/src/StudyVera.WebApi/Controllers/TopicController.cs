using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StudyVera.Application.Features.Topics.Queries;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetTopics(CancellationToken ct)
        {
            var response = await _mediator.Send(new GetAllTopicsQuery(), ct);
            return Ok(response);
        }

    }
}
