using Domain.AggregatesModel.WholesalerAggregate;

namespace Domain.AggregatesModel.WholesalerStockAggregate;

public class WholesalerStock : Entity, IAggregateRoot
{
    public Guid WholesalerId { get; private set; }
    public Guid BeerId { get; private set; }
    public double Quantity { get; private set; }
    public bool Deleted { get; private set; } = false;
    public Wholesaler Wholesaler { get; private set; } = default!;
    public Beer Beer { get; private set; } = default!;

    private WholesalerStock() { }
}
