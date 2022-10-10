using Domain.AggregatesModel.BeerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Beers;

public class BeerEntityTypeConfiguration : IEntityTypeConfiguration<Beer>
{
    public void Configure(EntityTypeBuilder<Beer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Deleted).HasDefaultValue(false);

        builder.HasOne(beer => beer.Brewery)
               .WithMany(brewery => brewery.Beers)
               .HasForeignKey(beer => beer.BreweryId);
    }
}
