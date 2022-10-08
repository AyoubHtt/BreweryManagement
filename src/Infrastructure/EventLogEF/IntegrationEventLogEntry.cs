using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.EventLogEF;

public class IntegrationEventLogEntry
{
    public Guid EventId { get; private set; }
    public Guid TransactionId { get; private set; }
    public string? EntityId { get; private set; } = default!;
    public string EventTypeName { get; private set; } = default!;
    public DateTime CreationTime { get; private set; }
    public string Content { get; private set; } = default!;

    private IntegrationEventLogEntry() { }

    public IntegrationEventLogEntry(Guid transactionId, string eventTypeName, DateTime creationTime, object @event, Guid entityId)
    {
        TransactionId = transactionId;
        EventTypeName = eventTypeName;
        CreationTime = creationTime;
        EntityId = entityId.ToString();
        Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
