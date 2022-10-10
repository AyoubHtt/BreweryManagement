using Domain.AggregatesModel.WholesalerStockAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.WholesalerStocks;

public class WholesalerStockEntityTypeConfiguration : IEntityTypeConfiguration<WholesalerStock>
{
    public void Configure(EntityTypeBuilder<WholesalerStock> builder)
    {
        builder.HasOne(wholesalerStock => wholesalerStock.Wholesaler)
               .WithMany(wholesaler => wholesaler.WholesalerStocks)
               .HasForeignKey(wholesalerStock => wholesalerStock.WholesalerId);

        builder.HasOne(wholesalerStock => wholesalerStock.Beer)
               .WithMany(beer => beer.WholesalerStocks)
               .HasForeignKey(wholesalerStock => wholesalerStock.BeerId);
    }
}
