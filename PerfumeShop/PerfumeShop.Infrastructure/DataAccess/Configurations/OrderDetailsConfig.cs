namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class OrderDetailsConfig : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {        
        builder.Property(p => p.Id)
            .IsRequired(true);

        builder.Property(p => p.OrderId)
            .IsRequired(true);

		#region Addressee 
		builder.OwnsOne(o => o.Addressee, a =>
		{
            a.Property(a => a.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(256)
                .IsRequired(true);

            a.Property(a => a.LastName)
                .HasColumnName("LastName")
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
        
        builder.Navigation(x => x.Addressee).IsRequired();
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
    }
}