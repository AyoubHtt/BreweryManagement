using Domain.Events.BreweryEvents;
using Domain.SeedWork;

namespace Domain.AggregatesModel.BreweryAggregate;

public class Brewery : Entity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public bool Deleted { get; private set; } = false;

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
}
