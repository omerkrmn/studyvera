using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.Analysis;

public class InsufficientTopicDto
{
    public string TopicName { get; set; } = string.Empty;
    public float InsufficiencyRate { get; set; }
}
