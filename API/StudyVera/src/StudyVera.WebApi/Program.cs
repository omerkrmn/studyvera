using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using StudyVera.Domain.Entities.Identity;
using StudyVera.Infrastructure.Persistence;
using StudyVera.WebApi.Extensions;
using StudyVera.WebApi.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyVera API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token giriniz. Örn: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureUnitOfWork(); 
builder.Services.ConfigureIdentity();
builder.Services.ConfigureMediatR();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureJwtSettings(builder.Configuration);
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("allow-all", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.ConfigureExceptionHandler();



//if (app.Environment.IsDevelopment())
//{
app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseHttpsRedirection();

app.UseCors("allow-all");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
