using Microsoft.Extensions.DependencyInjection;
using StudyVera.Application.Mappings;
using StudyVera.WebApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureUnitOfWork(); 
builder.Services.ConfigureIdentity();
builder.Services.ConfigureMediatR();
builder.Services.AddAutoMapper(typeof(MappingProfile));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
