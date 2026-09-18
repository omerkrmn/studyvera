using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;

namespace StudyVera.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext dbContext, UserManager<AppUser> manager)
    {
    }
}
