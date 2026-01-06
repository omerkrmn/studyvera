using Microsoft.AspNetCore.Mvc;
using StudyVera.Domain.Enums;
using System.Security.Claims;

namespace StudyVera.WebApi.Controllers.Common;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected Guid CurrentUserId =>
        Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id)
        ? id : throw new UnauthorizedAccessException("User ID claim is missing.");

    protected TargetExam CurrentTargetExam =>
        Enum.TryParse(User.FindFirst("TargetExam")?.Value, true, out TargetExam exam)
        ? exam : throw new BadHttpRequestException("Target exam claim is invalid or missing.");
}