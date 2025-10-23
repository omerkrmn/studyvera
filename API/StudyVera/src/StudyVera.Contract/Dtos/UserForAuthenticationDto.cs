using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Contract.Dtos;

public class UserForAuthenticationDto
{
    [Required(ErrorMessage = "E-Mail address cannot be null!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password cannot be null!")]
    public string Password { get; set; }
}