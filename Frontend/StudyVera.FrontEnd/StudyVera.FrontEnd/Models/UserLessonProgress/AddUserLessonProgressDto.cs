using StudyVera.FrontEnd.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StudyVera.FrontEnd.Models.UserLessonProgress;

public class AddUserLessonProgressDto
{

    [Required(ErrorMessage = "TopicId is required.")]
    public int TopicId { get; set; }

    [Required(ErrorMessage = "ProgressStatus is required.")]
    public ProgressStatus ProgressStatus { get; set; }
}
