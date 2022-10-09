using Infrastructure.EventLogEF.Events;

namespace Infrastructure.EventLogEF.Services;

public interface IIntegrationEventLogService
{
    Task SaveEventLogAsync(EventLogs eventLogs, CancellationToken cancellationToken);
}
