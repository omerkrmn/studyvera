using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos;

public class TopicDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int LessonId { get; set; }
    [Range(1,5)]
    public byte Priority { get; set; } 
}
