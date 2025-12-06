using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.QuestionStatDetails.Queries;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionStatDetailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionStatDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("{questionStatId}")]
        public async Task<IActionResult> GetQuestionStatDetailsByQuestionStatIdAsync([FromRoute] int questionStatId)
        {
            var query = new GetAllByUserQuestionStatQuery
            {
                QuestionStatId = questionStatId
            };

            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound(); 
            
            return Ok(response);
        }
    }
}
