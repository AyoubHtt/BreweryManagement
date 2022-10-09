using Domain.AggregatesModel.BreweryAggregate;

namespace Infrastructure.Repositories;

public class BreweryRepository : Repository<Brewery>, IBreweryRepository
{
    public BreweryRepository(BreweryContext breweryContext) : base(breweryContext) { }

    public async Task<bool> CheckAnotherBreweryExistByNameAsync(Guid id, string name, CancellationToken cancellationToken)
        => await DbSet.Where(x => x.Id != id && x.Name == name).AnyAsync(cancellationToken);

    public async Task<bool> CheckBreweryExistByNameAsync(string name, CancellationToken cancellationToken)
        => await DbSet.Where(x => x.Name == name).AnyAsync(cancellationToken);
}
