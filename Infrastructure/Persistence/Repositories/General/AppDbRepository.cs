using Application.Abstractions.Repositories.General;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.General;

public class AppDbRepository<T> : IRepository<T> where T: class
{
    private readonly DbSet<T> _dbSet;

    public AppDbRepository(AppDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
      => await _dbSet.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public void Update(T entity)
        => _dbSet.Update(entity);

    public void Remove(T entity)
        => _dbSet.Remove(entity);
}
