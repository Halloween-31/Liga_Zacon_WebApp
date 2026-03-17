using Application.Abstractions.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly AppDbContext _context;

    public DatabaseInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Articles.AnyAsync(cancellationToken))
        {
            return;
        }

        var articles = new List<Article>();
        for (int i = 1; i <= 100; i++)
        {
            articles.Add(new Article
            {
                Title = $"Article number {i}",
                Text = $"This is the text of article number {i}.",
                CreatedAt = DateTime.UtcNow,
                IsPublished = i % 2 == 0,
                Tag = i % 2 == 0 ? "Even" : "Odd"
            });
        }

        await _context.Articles.AddRangeAsync(articles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
