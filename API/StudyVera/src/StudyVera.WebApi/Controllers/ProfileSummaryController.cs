using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.ProfileSummary.Queries;
using StudyVera.Domain.Enums;
using System.Security.Claims;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileSummaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileSummaryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet("summary")]
        public async Task<IActionResult> GetProfileSummary([FromQuery] GetProfileSummaryQuery query, CancellationToken cancellationToken)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var targetExamClaim = User.FindFirst("TargetExam")?.Value;

            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token."); 

            if (!Guid.TryParse(userIdClaim, out var userIdGuid))
                return BadRequest("Invalid User ID format in token.");

            query.UserId = userIdGuid;

            if (targetExamClaim == null ||
                !Enum.TryParse(targetExamClaim, ignoreCase: true, out TargetExam targetExamType)) 
            {
                return BadRequest("Invalid or missing TargetExam claim in token.");
            }

            query.TargetExam = targetExamType;

            var response = await _mediator.Send(query, cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
