using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.UserQuestionStats.Commands;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserQuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("solve")]
        public async Task<IActionResult> SolveQuestion([FromBody] SolveQuestionCommand command, CancellationToken ct)
        {
            // JWT varsa buradan UserId’yi al
            // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // command.UserId = Guid.Parse(userId);

            var result = await _mediator.Send(command, ct);
            return result ? Ok("İstatistik güncellendi") : BadRequest("İşlem başarısız");
        }
    }
}
