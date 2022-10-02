using Infrastructure;

namespace API.Infrastructure.DbContextConfiguration;

public static class BreywerDbContextConfiguration
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Brewery_Connection_String");

        services.AddDbContext<BreweryContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
