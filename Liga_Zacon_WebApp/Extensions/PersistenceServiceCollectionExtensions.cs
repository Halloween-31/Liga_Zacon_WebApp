using Microsoft.EntityFrameworkCore;

namespace Liga_Zacon_WebApp.Extensions;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var test = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<Infrastructure.Persistence.AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}

