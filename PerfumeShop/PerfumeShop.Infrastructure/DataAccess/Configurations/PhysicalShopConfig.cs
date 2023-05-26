namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class PhysicalShopConfig : IEntityTypeConfiguration<PhysicalShop>
{
    public void Configure(EntityTypeBuilder<PhysicalShop> builder)
    {
        builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
	        .IsRequired();

        builder.Property(p => p.OpenTime)
            .IsRequired();

        builder.Property(p => p.CloseTime)
            .IsRequired();

        builder.Property(p => p.Weekends);

        #region Address
        builder.OwnsOne(o => o.Address, a =>
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

        builder.Navigation(x => x.Address).IsRequired();
        #endregion
    }
}
