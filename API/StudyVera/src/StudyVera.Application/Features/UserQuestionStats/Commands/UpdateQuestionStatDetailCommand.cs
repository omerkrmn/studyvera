using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.UserQuestionStats.Commands
{
    public class UpdateQuestionStatDetailCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        
        [JsonIgnore]
        public int QuestionStatDetailId { get; set; }

        [Required(ErrorMessage = "SolvedCount is required")]
        public int SolvedCount { get; set; }

        [Required(ErrorMessage = "CorrectCount is required")]
        public int CorrectCount { get; set; }

        public int? DurationMinutes { get; set; }
    }
}
