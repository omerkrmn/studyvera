using MediatR;
using StudyVera.Application.Dtos.ProfileSummary;
using StudyVera.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyVera.Application.Features.ProfileSummary.Queries;

public class GetProfileSummaryQuery : IRequest<ProfileViewModel>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    [JsonIgnore]
    public TargetExam TargetExam { get; set; }
}
