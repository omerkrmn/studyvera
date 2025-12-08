using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.Lessons.Commands;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/exams")]
    [ApiController]
    public class ExamControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExamControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
