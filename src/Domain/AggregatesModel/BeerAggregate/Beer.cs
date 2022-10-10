using Domain.AggregatesModel.OrderAggregate;

namespace Domain.AggregatesModel.BeerAggregate;

public class Beer : Entity, IAggregateRoot
{
    public Guid BreweryId { get; private set; }
    public string Name { get; private set; } = default!;
    public double Quantity { get; private set; }
    public double Price { get; private set; }
    public double SellPrice { get; private set; }
    public double AlcoholContent { get; private set; }
    public bool Deleted { get; private set; } = false;
    public Brewery Brewery { get; private set; } = default!;

    private readonly List<WholesalerStock> _wholesalerStocks = new();
    public IReadOnlyCollection<WholesalerStock> WholesalerStocks => _wholesalerStocks;

    private readonly List<Order> _orders = new();
    public IReadOnlyCollection<Order> Orders => _orders;

    private Beer() { }
}
