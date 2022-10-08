using Domain.SeedWork;

namespace Domain.AggregatesModel.BreweryAggregate;

public interface IBreweryRepository : IRepository<Brewery>
{
    /// <summary>
    /// Check if the given name already used for an existing brewery contrasting to specific brewery
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> CheckAnotherBreweryExistByNameAsync(Guid id, string name, CancellationToken cancellationToken);

    /// <summary>
    /// Check if the given name already used for an existing brewery
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <param name="cancellationToken"></param>
    Task<bool> CheckBreweryExistByNameAsync(string name, CancellationToken cancellationToken);
}
