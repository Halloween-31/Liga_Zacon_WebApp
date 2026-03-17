using Application.Abstractions.Repositories.ByEntities;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.General;

namespace Infrastructure.Persistence.Repositories.ByEntities;
public class ProductRepository : AppDbRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
