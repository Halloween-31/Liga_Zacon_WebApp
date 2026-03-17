using Application.Abstractions.Repositories.ByEntities;
using Application.Abstractions.Repositories.UnitOfWork;

namespace Infrastructure.Persistence.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public AppDbContext _context { get; }

    public IArticleRepository ArticleRepository { get; }
    public IProductRepository ProductRepository { get; }

    public UnitOfWork(AppDbContext context, IArticleRepository articleRepository, IProductRepository productRepository)
    {
        _context = context;
        ArticleRepository = articleRepository;
        ProductRepository = productRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
