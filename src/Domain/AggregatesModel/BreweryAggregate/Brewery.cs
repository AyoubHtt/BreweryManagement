using Domain.AggregatesModel.InvoiceAggregate;
using Domain.Events.BreweryEvents;

namespace Domain.AggregatesModel.BreweryAggregate;

public class Brewery : Entity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public bool Deleted { get; private set; } = false;

    private readonly List<Beer> _beers = new();
    public IReadOnlyCollection<Beer> Beers => _beers;

    private readonly List<Invoice> _invoices = new();
    public IReadOnlyCollection<Invoice> Invoices => _invoices;

    private Brewery() { }

    public Brewery(string name)
    {
        Name = name;

        AddDomainEvent(new BreweryCreatedEvent(this));
    }

    public void Update(string name)
    {
        Name = name;

        AddDomainEvent(new BreweryUpdatedEvent(this));
    }

    public void Delete()
    {
        Deleted = true;

        AddDomainEvent(new BreweryDeletedEvent(this));
    }
}
