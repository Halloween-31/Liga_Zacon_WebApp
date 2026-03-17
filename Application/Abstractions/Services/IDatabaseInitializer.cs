namespace Application.Abstractions.Services;

public interface IDatabaseInitializer
{
    Task ArticleSeedAsync(CancellationToken cancellationToken = default);
    Task ProductSeedAsync(CancellationToken cancellationToken = default);
}
