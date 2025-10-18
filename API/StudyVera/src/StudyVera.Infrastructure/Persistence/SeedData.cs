using Microsoft.AspNetCore.Identity;
using StudyVera.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyVera.Infrastructure.Persistence
{
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

}
