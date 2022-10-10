using Domain.AggregatesModel.OrderAggregate;

namespace Domain.AggregatesModel.InvoiceAggregate;

public class Invoice : Entity, IAggregateRoot
{
    public Guid BreweryId { get; private set; }
    public Guid WholesalerId { get; private set; }
    public Brewery Brewery { get; private set; } = default!;
    public Wholesaler Wholesaler { get; private set; } = default!;

    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders;

    private Invoice() { }
}
