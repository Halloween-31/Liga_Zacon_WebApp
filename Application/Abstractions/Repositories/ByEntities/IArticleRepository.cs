using Application.Abstractions.Repositories.General;
using Domain.Entities;

namespace Application.Abstractions.Repositories.ByEntities;

public interface IArticleRepository  : IRepository<Article>
{
    Task<IReadOnlyList<Article>> SearchByTagAsync(string tag, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> SearchByTitleAsync(string title, CancellationToken cancellationToken = default);
}
