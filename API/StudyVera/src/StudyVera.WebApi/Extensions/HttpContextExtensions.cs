using StudyVera.Domain.Enums;
using System.Security.Claims;

namespace StudyVera.WebApi.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        var claimValue = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(claimValue, out var id)
            ? id
            : throw new UnauthorizedAccessException("User ID claim is missing.");
    }

    public static TargetExam GetTargetExam(this HttpContext context)
    {
        var claimValue = context.User.FindFirst("TargetExam")?.Value;
        return Enum.TryParse(claimValue, true, out TargetExam exam)
            ? exam
            : throw new BadHttpRequestException("Target exam claim is invalid or missing.");
    }
}
