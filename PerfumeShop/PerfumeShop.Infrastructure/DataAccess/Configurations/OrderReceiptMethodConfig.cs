namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderDeliveryMethodConfig : IEntityTypeConfiguration<OrderDeliveryMethod>
{
    public void Configure(EntityTypeBuilder<OrderDeliveryMethod> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
		   .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
