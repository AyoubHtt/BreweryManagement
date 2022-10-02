using Infrastructure;

namespace API.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly BreweryContext _dbContext;

    public TransactionBehavior(BreweryContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);

        if (_dbContext.HasActiveTransaction)
        {
            return await next();
        }

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await _dbContext.BeginTransactionAsync();

            response = await next();

            await _dbContext.CommitTransactionAsync(transaction);
        });

        return response ?? default!;
    }
}
