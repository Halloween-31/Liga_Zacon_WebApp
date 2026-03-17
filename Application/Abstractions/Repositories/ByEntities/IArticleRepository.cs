using Application.Abstractions.Repositories.General;
using Domain.Entities;

namespace Application.Abstractions.Repositories.ByEntities;

public interface IArticleRepository  : IRepository<Article>
{
    Task<IReadOnlyList<Article>> SearchByTagAsync(string tag, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> SearchByTitleAsync(string title, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> GetPagedListAsync(int page = 0, int size = 10,
        string searchTerm = "",
        CancellationToken cancellationToken = default);
    Task<int> GetItemsCount(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetAllTagsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Article>> GetPagedListByTagAsync(int page = 0, int size = 10,
        string tag = "",
        CancellationToken cancellationToken = default);
}
