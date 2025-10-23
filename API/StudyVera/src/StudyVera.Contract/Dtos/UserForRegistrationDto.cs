using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Contract.Dtos;
public class UserForRegistrationDto
{

    // auth 
    [Required(ErrorMessage = "Firstname cannot be null!")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "LastName cannot be null!")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "UserName cannot be null!")]
    public string UserName { get; set; }
    public ExamTarget TargetExam { get; set; }
    [Required(ErrorMessage = "E-Mail address cannot be null!")]
    [EmailAddress]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password cannot be null!")]
    public string Password { get; set; }
}
