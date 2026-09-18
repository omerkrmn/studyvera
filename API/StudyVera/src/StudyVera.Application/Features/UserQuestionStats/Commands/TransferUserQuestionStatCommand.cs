using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.UserQuestionStats.Commands
{
    public class TransferUserQuestionStatCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "QuestionStatDetailId is required")]
        public int QuestionStatDetailId { get; set; }

        [Required(ErrorMessage = "NewTopicId is required")]
        public int NewTopicId { get; set; }
    }
}
