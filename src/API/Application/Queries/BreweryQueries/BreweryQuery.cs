using Domain.AggregatesModel.BreweryAggregate;
using Microsoft.AspNetCore.OData.Query;

namespace API.Application.Queries.BreweryQueries;

public record BreweryQuery(ODataQueryOptions<Brewery>? ODataQueryOptions) : IRequest<IQueryable>;

public class BreweryQueryHandler : IRequestHandler<BreweryQuery, IQueryable>
{
    private readonly IBreweryRepository _breweryRepository;

    public BreweryQueryHandler(IBreweryRepository breweryRepository) 
        => _breweryRepository = breweryRepository ?? throw new ArgumentNullException(nameof(breweryRepository));

    public Task<IQueryable> Handle(BreweryQuery request, CancellationToken cancellationToken) 
        => Task.FromResult(_breweryRepository.DbSet.AsQueryable().ApplyODataFilter(request.ODataQueryOptions));
}
