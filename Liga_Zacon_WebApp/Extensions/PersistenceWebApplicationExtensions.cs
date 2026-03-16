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

        return app;
    }
}

