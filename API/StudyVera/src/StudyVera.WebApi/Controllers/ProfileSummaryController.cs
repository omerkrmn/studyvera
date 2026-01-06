using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.ProfileSummary.Queries;
using StudyVera.Domain.Enums;
using StudyVera.WebApi.Controllers.Common;
using System.Security.Claims;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/profile")]
    [Authorize]
    public class ProfileSummaryController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileSummaryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("summary")]
        public async Task<IActionResult> GetProfileSummary([FromQuery] GetProfileSummaryQuery query, CancellationToken ct)
        {
            query.UserId = CurrentUserId;
            query.TargetExam = CurrentTargetExam;

            var response = await _mediator.Send(query, ct);
            return response != null ? Ok(response) : NotFound();
        }
    }
}
