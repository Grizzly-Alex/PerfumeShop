namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
		builder.Metadata.FindNavigation(nameof(Order.OrderItems))
			!.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(p => p.Id)
            .IsRequired(true);

        builder.Property(p => p.OrderDate)
            .HasColumnType("datetime2")
            .HasPrecision(0)
            .IsRequired(true);

		builder.HasOne(p => p.OrderStatus)
			.WithMany()
			.HasForeignKey(p => p.OrderStatusId)
			.OnDelete(DeleteBehavior.Restrict);

		#region Buyer info 
		builder.OwnsOne(o => o.BuyerInfo, a =>
		{
            a.Property(b => b.BuyerId)
				.HasColumnName("BuyerId")
                .IsRequired(true)
				.HasMaxLength(256);

            a.Property(a => a.BuyerName)
				.HasColumnName("BuyerName")
				.HasMaxLength(256)
				.IsRequired(true);

			a.Property(a => a.BuyerSurname)
				.HasColumnName("BuyerSurname")
				.HasMaxLength(256)
				.IsRequired(true);

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

			a.Property(a => a.PhoneNumber)
				.HasColumnName("PhoneNumber")
				.HasMaxLength(256)
				.IsRequired(true);
		});
        
        builder.Navigation(x => x.BuyerInfo).IsRequired();
        #endregion

        #region Shipping info 
        builder.OwnsOne(o => o.ShippingInfo, a =>
		{
			a.WithOwner();

			a.Property(p => p.ShippingDate)
				.HasColumnName("ShippingDate")
				.HasColumnType("datetime2")
				.HasPrecision(0)
                .IsRequired(false);

            a.Property(a => a.TrackingNumber)
				.HasColumnName("TrackingNumber")
				.HasMaxLength(256)
				.IsRequired(false);

			a.Property(a => a.Carrier)
				.HasColumnName("Carrier")
				.HasMaxLength(256)
				.IsRequired(false);		
		});
        
        builder.Navigation(x => x.ShippingInfo).IsRequired();
        #endregion

        #region Payment info
        builder.OwnsOne(o => o.PaymentInfo, a =>
		{
			a.WithOwner();

			a.Property(p => p.PaymentDate)
				.HasColumnName("PaymentDate")
				.HasColumnType("datetime2")
				.HasPrecision(0)
                .IsRequired(false);

            a.Property(p => p.PayablePrice)
				.HasColumnName("PayablePrice")
                .HasColumnType("decimal")
				.HasPrecision(10, 2)
				.IsRequired(true);

			a.Property(a => a.PaymentIntentId)
			   .HasColumnName("PaymentIntentId")
				.HasMaxLength(256)
				.IsRequired(false);

			a.Property(a => a.PaymentStatusId)
			   .HasColumnName("PaymentStatusId");
	
			a.HasOne(a => a.PaymentStatus)
				.WithMany()
				.HasForeignKey(o => o.PaymentStatusId)
				.OnDelete(DeleteBehavior.Restrict);
		});
        
        builder.Navigation(x => x.PaymentInfo).IsRequired();
        #endregion      			
	}
}