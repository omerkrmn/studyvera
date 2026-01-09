using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.UserProfiles.Commands;
using StudyVera.Application.Features.UserProfiles.Queries;
using StudyVera.WebApi.Controllers.Common;
using System.Security.Claims;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/profile/user-profile")]
    [ApiController]
    public class UserProfileController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile(CancellationToken ct)
        {
            var query = new GetUserProfileQuery
            {
                UserId = CurrentUserId
            };
            
            var response = await _mediator.Send(query, ct);

            return Ok();
        }
        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchProfile([FromBody] UpdateUserProfileCommand command)
        {
            command.UserId = CurrentUserId;
            var result = await _mediator.Send(command);
            return result ? NoContent() : BadRequest();
        }
    }
}
