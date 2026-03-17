using Application.Abstractions.Repositories.ByEntities;
using Application.Abstractions.Repositories.UnitOfWork;
using Application.Abstractions.Services;
using Application.Services;
using Infrastructure.Persistence.Repositories.ByEntities;
using Infrastructure.Persistence.Repositories.UnitOfWork;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Liga_Zacon_WebApp.Extensions;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var test = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<Infrastructure.Persistence.AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IArticleRepository, ArticleRepository>();

        services.AddScoped<IArticleService, ArticleService>();

        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

        return services;
    }
}

