using Domain.AggregatesModel.BreweryAggregate;

namespace Infrastructure.Repositories;

public class BreweryRepository : Repository<Brewery>, IBreweryRepository
{
    public BreweryRepository(BreweryContext breweryContext) : base(breweryContext) { }

    public async Task<bool> CheckBreweryExistByName(string name) => await DbSet.Where(x => x.Name == name).AnyAsync();
}
