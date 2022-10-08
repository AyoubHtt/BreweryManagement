using Microsoft.AspNetCore.OData.Query;

namespace API.Application.Extentions.ODataExtentions;

public static class ODataFilterExtention
{
    public static IQueryable ApplyODataFilter<T>(this IQueryable<T> queryable, ODataQueryOptions<T>? oDataQueryOptions) where T : Entity 
        => oDataQueryOptions is null ? queryable : oDataQueryOptions.ApplyTo(queryable.AsNoTracking());
}
