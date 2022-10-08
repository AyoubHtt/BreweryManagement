using Infrastructure.EventLogEF.Events;
using Infrastructure.EventLogEF.Services;
using MediatR;

namespace Infrastructure.EventLogEF.DomainEventHandlers;

public class EventLogHandler : INotificationHandler<EventLogs>
{
    private readonly BreweryContext _breweryContext;
    private readonly IIntegrationEventLogService _integrationEventLogService;

    public EventLogHandler(BreweryContext breweryContext, IIntegrationEventLogService integrationEventLogService)
    {
        _breweryContext = breweryContext ?? throw new ArgumentNullException(nameof(breweryContext));
        _integrationEventLogService = integrationEventLogService ?? throw new ArgumentNullException(nameof(integrationEventLogService));
    }

    public async Task Handle(EventLogs notification, CancellationToken cancellationToken)
        => await _integrationEventLogService.SaveEventLogAsync(notification);
}
