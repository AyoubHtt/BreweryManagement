using Domain.AggregatesModel.InvoiceAggregate;

namespace Domain.AggregatesModel.OrderAggregate;

public class Order : Entity, IAggregateRoot
{
    public Guid InvoiceId { get; private set; }
    public Guid BeerId { get; private set; }
    public double Quantity { get; private set; }
    public double UnityPrice { get; private set; }
    public Invoice Invoice { get; private set; } = default!;
    public Beer Beer { get; private set; } = default!;

    private Order() { }
}
