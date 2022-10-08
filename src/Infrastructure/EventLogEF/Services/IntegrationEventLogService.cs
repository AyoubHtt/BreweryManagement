using Infrastructure.EventLogEF.Events;
using Infrastructure.EventLogEF.Extensions;

namespace Infrastructure.EventLogEF.Services;

public class IntegrationEventLogService : IIntegrationEventLogService
{
    private readonly BreweryContext _breweryContext;

    public IntegrationEventLogService(BreweryContext breweryContext) => _breweryContext = breweryContext;

    public async Task SaveEventLogAsync(EventLogs eventLogs)
    {
        var currentDate = DateTime.Now;

        var entityId = eventLogs.Id;

        var eventLogEntries = eventLogs.LogEvents.Select(@event => new IntegrationEventLogEntry(_breweryContext.GetCurrentTransaction().TransactionId, @event.GetGenericTypeName(), currentDate, @event, entityId));

        await InsertLogs(eventLogEntries);
    }

    private async Task InsertLogs(IEnumerable<IntegrationEventLogEntry> eventLogEntries)
    {
        // Disable change tracking to make the insert faster
        _breweryContext.ChangeTracker.AutoDetectChangesEnabled = false;

        await _breweryContext.AddRangeAsync(eventLogEntries);

        _breweryContext.ChangeTracker.DetectChanges();

        await _breweryContext.SaveChangesAsync();

        _breweryContext.ChangeTracker.AutoDetectChangesEnabled = true;
    }
}
