using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Enums;

namespace StudyVera.Infrastructure.Persistence;

public static class SeedData
{
    public static async Task SeedAsync(AppDbContext dbContext, UserManager<AppUser> manager)
    {
        var user = new AppUser
        {
            UserName = "Omer",
            Email = "drakken120@gmail.com",
            EmailConfirmed = true,
            TargetExam = TargetExam.KPSS,
        };
        await manager.CreateAsync(user, "string1");
        await dbContext.SaveChangesAsync();
    }
}
