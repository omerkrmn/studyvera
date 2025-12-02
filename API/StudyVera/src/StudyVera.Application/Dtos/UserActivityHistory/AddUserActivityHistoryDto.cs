using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Application.Dtos.UserActivityHistory;

public class AddUserActivityHistoryDto
{
    public DateTime ActivityDate { get; set; } = DateTime.UtcNow;
    public ActivityType? ActivityType { get; set; }
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}
