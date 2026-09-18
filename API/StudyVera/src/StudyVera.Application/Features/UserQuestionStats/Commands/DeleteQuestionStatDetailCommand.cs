using MediatR;
using System;

namespace StudyVera.Application.Features.UserQuestionStats.Commands
{
    public class DeleteQuestionStatDetailCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public int QuestionStatDetailId { get; set; }
    }
}
