using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos;

public record TokenDto
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
