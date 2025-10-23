using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Entities.Identity;

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
        };
        await manager.CreateAsync(user, "1652");
        await dbContext.SaveChangesAsync();
    }
}
