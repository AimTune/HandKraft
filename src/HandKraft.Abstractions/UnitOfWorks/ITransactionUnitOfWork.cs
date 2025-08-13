namespace HandKraft.Abstractions.UnitOfWorks;

public interface ITransactionUnitOfWork : IUnitOfWork
{
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
}