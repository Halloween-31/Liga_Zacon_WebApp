namespace Application.Abstractions.Services;

public interface IDatabaseInitializer
{
    Task SeedAsync(CancellationToken cancellationToken = default);
}
