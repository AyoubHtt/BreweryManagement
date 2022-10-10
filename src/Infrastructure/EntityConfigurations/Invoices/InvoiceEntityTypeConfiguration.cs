using Domain.AggregatesModel.InvoiceAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Invoices;

public class InvoiceEntityTypeConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(invoice => invoice.Brewery)
               .WithMany(brewery => brewery.Invoices)
               .HasForeignKey(invoice => invoice.BreweryId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(invoice => invoice.Wholesaler)
               .WithMany(wholesaler => wholesaler.Invoices)
               .HasForeignKey(invoice => invoice.WholesalerId)
               .OnDelete(DeleteBehavior.NoAction);
    }
}
