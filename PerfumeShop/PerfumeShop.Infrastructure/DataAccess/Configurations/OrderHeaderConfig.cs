namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderHeaderConfig : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired(true);

        builder.Property(b => b.CustomerId)
            .IsRequired(true)
            .HasMaxLength(256);

        builder.Property(b => b.EmployeeId)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.Property(p => p.OrderDate)
            .HasColumnType("datetime2")
            .HasPrecision(0)
            .IsRequired(true);

        builder.Property(p => p.ShippingDate)
            .HasColumnType("datetime2")
            .HasPrecision(0)
            .IsRequired(false);

        builder.Property(a => a.TrackingId)
            .HasMaxLength(256)
            .IsRequired(true);
      
        builder.HasOne(p => p.Details)
            .WithOne(d => d.Order)
            .HasForeignKey<OrderDetail>(e => e.OrderId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Payment)
            .WithOne(d => d.Order)
            .HasForeignKey<Payment>(e => e.OrderId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.OrderStatus)
			.WithMany()
			.HasForeignKey(p => p.OrderStatusId)
			.OnDelete(DeleteBehavior.Restrict);

        builder.Metadata.FindNavigation(nameof(OrderHeader.OrderItems))
            !.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}