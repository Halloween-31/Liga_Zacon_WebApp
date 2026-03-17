using Application.Abstractions.Repositories.ByEntities;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.General;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.ByEntities;

public class ArticleRepository : AppDbRepository<Article>, IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Article>> SearchByTagAsync(string tag, CancellationToken cancellationToken = default)
    {
        return await _context.Articles
            .Where(a => a.Tag != null && a.Tag.Contains(tag))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Article>> SearchByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Articles
            .Where(a => a.Title.Contains(title))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
