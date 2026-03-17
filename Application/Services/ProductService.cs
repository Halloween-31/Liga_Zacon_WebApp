using System.Data;
using Application.Abstractions.Repositories.UnitOfWork;
using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ProductRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _unitOfWork.ProductRepository.ListAsync(cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        try
        {
            _unitOfWork.ProductRepository.Update(product);
        }
        catch (DBConcurrencyException)
        {
            if ((await GetByIdAsync(product.Id, cancellationToken)) != null)
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
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _unitOfWork.ProductRepository.Remove(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
