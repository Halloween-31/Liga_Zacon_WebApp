using System.Data;
using Application.Abstractions.Repositories.ByEntities;
using Application.Abstractions.Repositories.UnitOfWork;
using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Services;
public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    public ArticleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Article?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ArticleRepository.GetByIdAsync(id, cancellationToken);
    }
    public async Task<IReadOnlyList<Article>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ArticleRepository.ListAsync(cancellationToken);
    }
    public async Task AddAsync(Article article, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ArticleRepository.AddAsync(article, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> UpdateAsync(Article article, CancellationToken cancellationToken = default)
    {
        try
        {
            _unitOfWork.ArticleRepository.Update(article);
        }
        catch (DBConcurrencyException)
        {
            if ((await GetByIdAsync(article.Id, cancellationToken)) != null)
            {
                return false;
            }
            else
            {
                throw;
            }
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        var article = await GetByIdAsync(id, cancellationToken);
        if (article == null)
            return false;

        _unitOfWork.ArticleRepository.Remove(article);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
    public async Task<IReadOnlyList<Article>> SearchByTagAsync(string tag, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ArticleRepository.SearchByTagAsync(tag, cancellationToken);
    }
    public async Task<IReadOnlyList<Article>> SearchByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ArticleRepository.SearchByTitleAsync(title, cancellationToken);
    }

    public async Task<IReadOnlyList<Article>> GetPagedListAsync(int page = 0, int size = 10,
        string searchTerm = "",
        CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ArticleRepository.GetPagedListAsync(page, size, searchTerm, cancellationToken);
    }

    public async Task<int> GetItemsCount()
    {
        return await _unitOfWork.ArticleRepository.GetItemsCount();
    }

    public async Task<IReadOnlyList<string>> GetAllTagsAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ArticleRepository.GetAllTagsAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Article>> GetPagedByTagsListAsync(int page = 0, int size = 10, string tag = "",
        CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ArticleRepository.GetPagedListByTagAsync(page, size, tag, cancellationToken);

    }
}
