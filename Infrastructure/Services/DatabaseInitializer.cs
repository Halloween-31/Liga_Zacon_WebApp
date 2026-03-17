using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Abstractions.Services;
using Domain.Entities;
using Infrastructure.Persistence;
using Liga_Zacon_WebApp.ApiContracts.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly AppDbContext _context;

    public DatabaseInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task ArticleSeedAsync(CancellationToken cancellationToken = default)
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

    public async Task ProductSeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Products.AnyAsync(cancellationToken))
        {
            return;
        }

        using var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync("https://fakestoreapi.com/products", cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            var productDto = JsonSerializer.Deserialize<IEnumerable<ProductApiResponseDTO>>(responseBody) ?? [];

            var products = new List<Product>();
            foreach (var dto in productDto)
            {
                products.Add(dto.ToEntity());
            }
            await _context.Products.AddRangeAsync(products, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
