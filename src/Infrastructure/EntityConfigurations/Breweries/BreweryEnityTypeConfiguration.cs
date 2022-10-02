using Domain.AggregatesModel.BreweryAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Breweries;

public class BreweryEnityTypeConfiguration : IEntityTypeConfiguration<Brewery>
{
    public void Configure(EntityTypeBuilder<Brewery> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=>x.Deleted).HasDefaultValue(false);
    }
}
