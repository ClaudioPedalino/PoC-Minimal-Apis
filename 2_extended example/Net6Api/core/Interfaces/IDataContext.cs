public interface IDataContext
{
    Task BeginTransactionAsync(CancellationToken cancellationToken);

    Task CommitTransactionAsync(CancellationToken cancellationToken);

    Task RollbackTransactionAsync(CancellationToken cancellationToken);

    Task RetryOnExceptionAsync(Func<Task> func);
}