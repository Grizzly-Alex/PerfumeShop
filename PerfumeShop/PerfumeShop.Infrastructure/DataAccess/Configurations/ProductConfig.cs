namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class ProductConfig : IEntityTypeConfiguration<CatalogProduct>
{
    public void Configure(EntityTypeBuilder<CatalogProduct> builder)
    {
        builder.Property(p => p.Id)
			.IsRequired();

		builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .IsRequired(true)
            .HasColumnType("decimal")
            .HasPrecision(10, 2);

        builder.Property(p => p.DiscountPercent)
            .IsRequired(true);
            
        builder.Property(p => p.Stock)
            .IsRequired(true);

        builder.Property(p => p.Volume)
            .IsRequired(true);

        builder.Property(p => p.DateDelivery)
            .HasColumnType("datetime2")
            .HasPrecision(0);

        builder.Property(p => p.PictureUri)
            .IsRequired(false);

        builder.HasOne(p => p.Brand)
            .WithMany()
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.AromaType)
            .WithMany()
            .HasForeignKey(p => p.AromaTypeId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.Gender)
            .WithMany()
            .HasForeignKey(p => p.GenderId)
            .OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(p => p.ReleaseForm)
            .WithMany()
            .HasForeignKey(p => p.ReleaseFormId)
			.OnDelete(DeleteBehavior.Restrict);
    }
}