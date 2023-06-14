namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderHeaderConfig : IEntityTypeConfiguration<OrderHeader>
{
    public void Configure(EntityTypeBuilder<OrderHeader> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired(true);

        builder.Property(p => p.OrderDate)
            .HasColumnType("datetime2")
            .HasPrecision(0)
            .IsRequired(true);

        builder.Property(b => b.EmployeeId)
            .IsRequired(false)
            .HasMaxLength(256);

        builder.Property(p => p.ShippingDate)
            .HasColumnType("datetime2")
            .HasPrecision(0)
            .IsRequired(false);

        builder.Property(a => a.OrderId)
            .HasMaxLength(256)
            .IsRequired(true);

        #region Address
        builder.OwnsOne(o => o.DeliveryAddress, a =>
        {
            a.Property(a => a.State)
                .HasColumnName("State")
                .HasMaxLength(256)
                .IsRequired(true);

            a.Property(a => a.City)
                .HasColumnName("City")
                .HasMaxLength(256)
                .IsRequired(true);

            a.Property(a => a.StreetAddress)
                .HasColumnName("StreetAddress")
                .HasMaxLength(256)
                .IsRequired(true);

            a.Property(a => a.PostalCode)
                .HasColumnName("PostalCode")
                .HasMaxLength(256)
                .IsRequired(true);
        });

        builder.Navigation(x => x.DeliveryAddress).IsRequired();
        #endregion

        #region Customer
        builder.OwnsOne(o => o.Customer, a =>
        {
            a.Property(a => a.UserId)
                .HasColumnName("UserId")
                .HasMaxLength(256)
                .IsRequired(true);

            a.Property(a => a.FirstName)
                .HasColumnName("CustomerName")
                .HasMaxLength(256)
                .IsRequired(true);

            a.Property(a => a.LastName)
                .HasColumnName("CustomerSurname")
                .HasMaxLength(256)
                .IsRequired(true);

            a.Property(a => a.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(256)
                .IsRequired(true);

			a.Property(a => a.ReceiptEmail)
				.HasColumnName("ReceiptEmail")
				.HasMaxLength(256)
				.IsRequired(true);
		});

        builder.Navigation(x => x.Customer).IsRequired();
        #endregion

        #region Cost
        builder.OwnsOne(o => o.Cost, a =>
        {
            a.Property(p => p.ItemsCost)
                .HasColumnName("ItemsCost")
                .IsRequired(true)
                .HasColumnType("decimal")
                .HasPrecision(10, 2);

            a.Property(p => p.ShippingCost)
                .HasColumnName("ShippingCost")
                .IsRequired(true)
                .HasColumnType("decimal")
                .HasPrecision(10, 2);

            a.Property(a => a.PromoCodeCost)
                .HasColumnName("PromoCodeCost")
                .IsRequired(true)
                .HasColumnType("decimal")
                .HasPrecision(10, 2);

            a.Property(a => a.TotalCost)
                .HasColumnName("TotalCost")
                .IsRequired(true)
                .HasColumnType("decimal")
                .HasPrecision(10, 2);
        });

        builder.Navigation(x => x.Cost).IsRequired();
        #endregion

        builder.HasOne(p => p.PaymentDetail)
            .WithOne(d => d.Order)
            .HasForeignKey<PaymentDetail>(e => e.OrderId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.DeliveryMethod)
			.WithMany()
			.HasForeignKey(p => p.DeliveryMethodId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.OrderStatus)
			.WithMany()
			.HasForeignKey(p => p.OrderStatusId)
			.OnDelete(DeleteBehavior.Restrict);

        builder.Metadata.FindNavigation(nameof(OrderHeader.OrderItems))
            !.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}