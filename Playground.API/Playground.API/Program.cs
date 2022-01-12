using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Playground.Core.Interfaces;
using Playground.Infrastructure.Data;

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

var app = builder.Build();

// Configure the HTTP request pipeline.

IWebHostEnvironment env = app.Environment;
using (var scope = app.Services.CreateScope())
{
    PlayDbContext dbContext = scope.ServiceProvider.GetRequiredService<PlayDbContext>();
    if (env.IsDevelopment())
    {
        await PlayDbContextSeed.CreateAndSeedDatabaseAsync(dbContext);
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
