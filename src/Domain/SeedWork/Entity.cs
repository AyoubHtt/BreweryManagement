namespace Domain.SeedWork;

public abstract class Entity
{
    [JsonInclude]
    public Guid Id { get; protected set; }

    private List<INotification> _domainEvents = default!;

    public IReadOnlyCollection<INotification> GetDomainEvents() => _domainEvents?.AsReadOnly() ?? default!;

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents ??= new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

    public void ClearDomainEvents() => _domainEvents?.Clear();
}
