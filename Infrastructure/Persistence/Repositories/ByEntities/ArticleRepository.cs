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
            .Where(a => a.Tag == tag)
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

    public async Task<IReadOnlyList<Article>> GetPagedListAsync(int page = 0, int size = 10,
        string searchTerm = "", 
        CancellationToken cancellationToken = default)
    {
        var q = _context.Articles.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            q = q.Where(a => a.Title.Contains(searchTerm));
        }

        return await q
            .Skip(page * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetItemsCount(CancellationToken cancellationToken = default)
    {
        return await _context.Articles
            .CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetAllTagsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Articles
            .Where(a => !string.IsNullOrEmpty(a.Tag))
            .Select(a => a.Tag!)
            .Distinct()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Article>> GetPagedListByTagAsync(int page = 0, int size = 10, string tag = "",
        CancellationToken cancellationToken = default)
    {
        var q = _context.Articles.AsQueryable();

        if (!string.IsNullOrEmpty(tag))
        {
            q = q.Where(a => a.Tag == tag);
        }

        return await q
            .Skip(page * size)
            .Take(size)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
