using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace Liga_Zacon_WebApp.Extensions;

public static class PersistenceWebApplicationExtensions
{
    public static WebApplication ApplyMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<Infrastructure.Persistence.AppDbContext>();
        context.Database.Migrate();

        if (app.Environment.IsDevelopment())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            initializer.SeedAsync().GetAwaiter().GetResult();
        }

        return app;
    }
}

