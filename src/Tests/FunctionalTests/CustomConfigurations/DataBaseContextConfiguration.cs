using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionalTests.CustomConfigurations;

public static class DataBaseContextConfiguration
{
    public static void AddCustomTestDbCotext(this IServiceCollection services)
    {
        var breweryDbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BreweryContext>));

        if (breweryDbDescriptor != null) services.Remove(breweryDbDescriptor);

        // Create Sqlite database folder
        string databaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "TestDatabase";
        Directory.CreateDirectory(databaseDirectory);

        services.AddDbContext<BreweryContext>(options =>
        {
            var conn = new SqliteConnection($"Data Source={databaseDirectory}\\SqLiteTest.db");
            conn.Open(); // open connection to use

            conn.CreateFunction("GETDATE", () => DateTime.Now);

            options.UseSqlite(conn);
        });
    }

    public static void InitializeTestDatabase(this IServiceProvider scopedServices)
    {
        var db = scopedServices.GetRequiredService<BreweryContext>();

        db.Database.EnsureCreated();
    }
}
