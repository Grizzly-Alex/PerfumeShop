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
            .HasPrecision(0);

        builder.Property(p => p.OrderTotal)
            .IsRequired(true)
            .HasColumnType("decimal")
            .HasPrecision(10, 2);

        builder.Property(b => b.OrderStatus)
            .IsRequired(true)
            .HasMaxLength(256);

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
				.IsRequired();

			a.Property(a => a.BuyerSurname)
				.HasColumnName("BuyerSurname")
				.HasMaxLength(256)
				.IsRequired();

            a.Property(a => a.State)
				.HasColumnName("State")
				.HasMaxLength(256)
				.IsRequired();

			a.Property(a => a.City)
				.HasColumnName("City")
				.HasMaxLength(256)
				.IsRequired();

			a.Property(a => a.StreetAddress)
				.HasColumnName("StreetAddress")
				.HasMaxLength(256)
				.IsRequired();

			a.Property(a => a.PostalCode)
				.HasColumnName("PostalCode")
				.HasMaxLength(256)
				.IsRequired();

			a.Property(a => a.PhoneNumber)
				.HasColumnName("PhoneNumber")
				.HasMaxLength(256)
				.IsRequired();
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
				.HasPrecision(0);

			a.Property(a => a.TrackingNumber)
				.HasColumnName("TrackingNumber")
				.HasMaxLength(256)
				.IsRequired();

			a.Property(a => a.Carrier)
				.HasColumnName("Carrier")
				.HasMaxLength(256)
				.IsRequired();		
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
				.HasPrecision(0);
		
			a.Property(a => a.PaymentStatus)
				.HasColumnName("PaymentStatus")
				.HasMaxLength(256)
				.IsRequired();

			a.Property(a => a.PaymentIntentId)
			   .HasColumnName("PaymentIntentId")
				.HasMaxLength(256)
				.IsRequired();			
		});
        
        builder.Navigation(x => x.PaymentInfo).IsRequired();
        #endregion      			
	}
}
