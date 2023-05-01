namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderStatusConfig : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
		   .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
