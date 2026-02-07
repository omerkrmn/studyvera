using Carter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyVera.Application.Features.Lessons.Commands;

namespace StudyVera.WebApi.Controllers
{
    [Route("api/exams")]
    [ApiController]
    public class ExamModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/auth")
                    .WithTags("Auth");

        }
    }
}
