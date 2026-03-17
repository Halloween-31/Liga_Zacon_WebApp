using Application.Abstractions.Repositories.ByEntities;
using Application.Abstractions.Repositories.UnitOfWork;

namespace Infrastructure.Persistence.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public AppDbContext _context { get; }

    public IArticleRepository ArticleRepository { get; }

    public UnitOfWork(AppDbContext context, IArticleRepository articleRepository)
    {
        _context = context;
        ArticleRepository = articleRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
