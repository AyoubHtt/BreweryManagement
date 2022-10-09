using Infrastructure.EventLogEF.Events;
using Infrastructure.EventLogEF.Extensions;

namespace Infrastructure.EventLogEF.Services;

public class IntegrationEventLogService : IIntegrationEventLogService
{
    private readonly BreweryContext _breweryContext;

    public IntegrationEventLogService(BreweryContext breweryContext) => _breweryContext = breweryContext;

    public async Task SaveEventLogAsync(EventLogs eventLogs, CancellationToken cancellationToken)
    {
        var currentDate = DateTime.Now;

        var entityId = eventLogs.Id;

        var eventLogEntries = eventLogs.LogEvents.Select(@event => new IntegrationEventLogEntry(_breweryContext.GetCurrentTransaction().TransactionId, @event.GetGenericTypeName(), currentDate, @event, entityId));

        await InsertLogs(eventLogEntries, cancellationToken);
    }

    private async Task InsertLogs(IEnumerable<IntegrationEventLogEntry> eventLogEntries, CancellationToken cancellationToken)
    {
        // Disable change tracking to make the insert faster
        _breweryContext.ChangeTracker.AutoDetectChangesEnabled = false;

        await _breweryContext.AddRangeAsync(eventLogEntries, cancellationToken);

        _breweryContext.ChangeTracker.DetectChanges();

        await _breweryContext.SaveChangesAsync(cancellationToken);

        _breweryContext.ChangeTracker.AutoDetectChangesEnabled = true;
    }
}
