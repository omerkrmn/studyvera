using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos;
public class UserForRegistrationDto
{

    // auth 
    [Required(ErrorMessage = "Firstname cannot be null!")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "LastName cannot be null!")]
    public string LastName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public TargetExam TargetExam { get; set; }
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password cannot be null!")]
    public string Password { get; set; } = string.Empty;
}
