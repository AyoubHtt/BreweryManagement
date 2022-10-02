using Domain.SeedWork;

namespace Domain.AggregatesModel.BreweryAggregate;

public interface IBreweryRepository : IRepository<Brewery>
{
    /// <summary>
    /// Check if the given name already used for an existing brewery
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Task<bool> CheckBreweryExistByName(string name);
}
