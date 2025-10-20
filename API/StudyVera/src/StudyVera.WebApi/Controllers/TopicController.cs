using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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


    }
}
