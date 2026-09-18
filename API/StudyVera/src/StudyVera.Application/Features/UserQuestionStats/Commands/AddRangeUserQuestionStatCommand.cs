using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyVera.Application.Features.UserQuestionStats.Commands
{
    public class AddRangeUserQuestionStatCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        
        public List<UserQuestionStatItem> Items { get; set; } = new();
    }

    public class UserQuestionStatItem
    {
        [Required(ErrorMessage = "TopicId is required")]
        public int TopicId { get; set; }
        
        [Required(ErrorMessage = "SolvedCount is required")]
        public int SolvedCount { get; set; }
        
        [Required(ErrorMessage = "CorrectCount is required")]
        public int CorrectCount { get; set; }
        
        public int? DurationMinutes { get; set; }
    }
}
