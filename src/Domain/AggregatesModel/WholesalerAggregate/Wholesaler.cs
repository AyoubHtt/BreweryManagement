using Domain.AggregatesModel.InvoiceAggregate;

namespace Domain.AggregatesModel.WholesalerAggregate;

public class Wholesaler : Entity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public bool Deleted { get; private set; } = false;

    private readonly List<WholesalerStock> _wholesalerStocks = new();
    public IReadOnlyCollection<WholesalerStock> WholesalerStocks => _wholesalerStocks;

    private readonly List<Invoice> _invoices = new();
    public IReadOnlyCollection<Invoice> Invoices => _invoices;

    private Wholesaler() { }
}
