using Domain.AggregatesModel.WholesalerAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations.Wholesalers;

public class WholesalerEntityTypeConfiguration : IEntityTypeConfiguration<Wholesaler>
{
    public void Configure(EntityTypeBuilder<Wholesaler> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Deleted).HasDefaultValue(false);
    }
}
