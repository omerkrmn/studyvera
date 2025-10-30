using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.UserLessonProgresses.Commands;
using StudyVera.Application.Features.UserLessonProgresses.Queries;
using StudyVera.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLessonProgressControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserLessonProgressControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery, Required] Guid userId, CancellationToken ct)
        {
            GetAllLessonProgressesQuery getAllLessonProgressesQuery1 = new()
            {
                UserId = userId
            };
            var response = await _mediator.Send(getAllLessonProgressesQuery1, ct);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateProgress([FromBody] AddUserLessonProgressCommand command,CancellationToken ct)
        {
            if (command is null)
                throw new ParameterNullException("Request body cannot be null!");

            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
    }
}
