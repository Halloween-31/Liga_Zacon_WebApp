using Domain.Entities;

namespace Application.Abstractions.Services;

public interface IArticleService
{
    Task<Article?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> ListAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Article article, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Article article, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> SearchByTagAsync(string tag, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> SearchByTitleAsync(string title, CancellationToken cancellationToken = default);
}
