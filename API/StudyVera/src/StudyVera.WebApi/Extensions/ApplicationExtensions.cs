using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudyVera.Application.Services;
using StudyVera.Domain.Entities;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Domain.Interfaces;
using StudyVera.Infrastructure.Identity;
using StudyVera.Infrastructure.Persistence.Repositories;
using System.Text;
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
        var builder = services.AddIdentity<AppUser, AppRole>(options =>
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

    public static void ConfigureUnitOfWork(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IAuthenticationManager, AuthenticationManager>();

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(StudyVera.Application.AssemblyReferance).Assembly));
    }

    public static void ConfigureJwtSettings(this IServiceCollection services, IConfiguration configuration)=>
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    
    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var secretKey = jwtSettings?.SecretKey;

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings!.ValidIssuer,
                ValidAudience = jwtSettings.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
            };
        });
    }

}
