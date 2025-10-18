using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyVera.Domain.Entities;
using StudyVera.Infrastructure.Persistence;
using AppDbContext = StudyVera.Infrastructure.Persistence.AppDbContext;

namespace StudyVera.WebApi.Extensions;

public static class ApplicationExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("sqlConnection"),
                 sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    }));
    }
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;

            options.User.RequireUniqueEmail = true;

        })
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();
    }
}
