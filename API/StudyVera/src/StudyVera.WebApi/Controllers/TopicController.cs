using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.Topics.Queries;
using StudyVera.Domain.Enums;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTopics([FromQuery] string? searchTerm, CancellationToken ct)
        {
            var examTargetClaim = User.FindFirst("TargetExam")?.Value;
            if (examTargetClaim == null)
                return Unauthorized("ExamTarget claim not found in token.");

            if (!Enum.TryParse<TargetExam>(examTargetClaim, true, out var examTarget))
                return BadRequest($"Invalid ExamTarget claim: {examTargetClaim}");

            var query = new GetAllTopicsQuery
            {
                TargetExam = examTarget,
                SearchTerm = searchTerm
            };

            var response = await _mediator.Send(query, ct);
            return Ok(response);
        }


    }
}
