namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class DeliveryDetailConfig : IEntityTypeConfiguration<DeliveryDetail>
{
    public void Configure(EntityTypeBuilder<DeliveryDetail> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired(true);

        builder.Property(p => p.DeliveryDate)
            .HasColumnName("DeliveryDate")
            .HasColumnType("datetime2")
            .HasPrecision(0)
            .IsRequired(false);

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

        builder.Property(a => a.DeliveryMethodId)
            .HasColumnName("DeliveryMethodId");

		builder.HasOne(a => a.DeliveryMethod)
            .WithMany()
            .HasForeignKey(o => o.DeliveryMethodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}