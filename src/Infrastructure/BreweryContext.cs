using Infrastructure.EntityConfigurations.Beers;
using Infrastructure.EntityConfigurations.Breweries;
using Infrastructure.EntityConfigurations.IntegrationEventLogEntries;
using Infrastructure.EntityConfigurations.Invoices;
using Infrastructure.EntityConfigurations.Orders;
using Infrastructure.EntityConfigurations.Wholesalers;
using Infrastructure.EntityConfigurations.WholesalerStocks;
using Infrastructure.EventLogEF.Extensions;
using MediatR;

namespace Infrastructure;

public class BreweryContext : DbContext, IUnitOfWork
{
    private IDbContextTransaction _currentTransaction = default!;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;
    private readonly IMediator _bus;

    public BreweryContext(DbContextOptions<BreweryContext> options, IMediator bus) : base(options) { _bus = bus; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BeerEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BreweryEnityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new IntegrationEventLogEntryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WholesalerEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WholesalerStockEntityTypeConfiguration());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await SaveChangesAsync(cancellationToken);

        await _bus.DispatchDomainEventsAsync(this);

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
