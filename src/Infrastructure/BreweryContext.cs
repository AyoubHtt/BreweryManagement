namespace Infrastructure;

public class BreweryContext : DbContext, IUnitOfWork
{
    private IDbContextTransaction _currentTransaction = default!;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;

    public BreweryContext(DbContextOptions<BreweryContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return default!;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        ValidateTransaction(transaction);

        try
        {
            await SaveChangesAsync();
            transaction.Commit();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = default!;
            }
        }
    }

    private void ValidateTransaction(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = default!;
            }
        }
    }
}
