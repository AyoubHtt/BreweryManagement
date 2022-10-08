using MediatR;

namespace Infrastructure.EventLogEF.Events;

public class EventLogs : INotification
{
    public List<INotification> LogEvents { get; }
    public Guid Id { get; }

    public EventLogs(List<INotification> logEvents, Guid id)
    {
        LogEvents = logEvents;
        Id = id;
    }
}
