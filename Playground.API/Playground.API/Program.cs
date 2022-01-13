using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Playground.Core.Interfaces;
using Playground.Infrastructure.Data;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddDbContext<PlayDbContext>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    b.LogTo(Console.WriteLine,
        new[]
        {
            DbLoggerCategory.Database.Command.Name,
        },
        LogLevel.Information,
        DbContextLoggerOptions.SingleLine | DbContextLoggerOptions.UtcTime);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Playground API",
        Version = "v1"
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

IWebHostEnvironment env = app.Environment;
using (var scope = app.Services.CreateScope())
{
    PlayDbContext dbContext = scope.ServiceProvider.GetRequiredService<PlayDbContext>();
    if (env.IsDevelopment())
    {
        await PlayDbContextSeed.CreateAndSeedDatabaseAsync(dbContext);

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Awesome API V1");
        });
    }

    if (env.IsProduction())
    {
        await dbContext.Database.MigrateAsync();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
