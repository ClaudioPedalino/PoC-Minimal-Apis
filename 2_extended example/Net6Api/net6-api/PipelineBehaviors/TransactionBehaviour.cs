public class TransactionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionable
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IDataContext _dbContext;
    private readonly EventStoreContext _eventContext;

    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger,
                               IDataContext dbContext,
                               EventStoreContext eventContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _eventContext = eventContext;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        TResponse response = default;
        try
        {
            await _dbContext.RetryOnExceptionAsync(async () =>
            {
                _logger.LogInformation($"Begin transaction: {typeof(TRequest).Name}.");
                await _dbContext.BeginTransactionAsync(cancellationToken);

                response = await next();
                _eventContext.AddEvent(typeof(TRequest).Name);
                await _dbContext.CommitTransactionAsync(cancellationToken);
                _logger.LogInformation($"End transaction: {typeof(TRequest).Name}.");
            });
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}.");
            await _dbContext.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(e.Message, e.StackTrace);

            throw;
        }

        return response;
    }
}