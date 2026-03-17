using Application.Abstractions.Repositories.ByEntities;

namespace Application.Abstractions.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    IArticleRepository ArticleRepository { get; }
    IProductRepository ProductRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
