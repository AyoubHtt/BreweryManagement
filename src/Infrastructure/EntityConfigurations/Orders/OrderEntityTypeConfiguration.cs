using Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Orders;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(order => order.Invoice)
               .WithMany(invoice => invoice.Orders)
               .HasForeignKey(order => order.InvoiceId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(order => order.Beer)
               .WithMany(beer => beer.Orders)
               .HasForeignKey(order => order.BeerId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
