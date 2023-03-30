namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class ProductConfig : IEntityTypeConfiguration<CatalogProduct>
{
    public void Configure(EntityTypeBuilder<CatalogProduct> builder)
    {
        builder.ToTable("Catalog");

        builder.Property(p => p.Id)
            .UseHiLo("catalog_hilo")
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired(true)
            .HasMaxLength(100);

        builder.Property(p => p.Price)
            .IsRequired(true)
            .HasColumnType("decimal")
            .HasPrecision(10, 2);

        builder.Property(p => p.Stock)
            .IsRequired(true);

        builder.Property(p => p.Volume)
            .IsRequired(true);

        builder.Property(p => p.PictureUri)
            .IsRequired(false);

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId);

        builder.HasOne(p => p.Brand)
            .WithMany()
            .HasForeignKey(p => p.BrandId);

        builder.HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId);

        builder.HasOne(p => p.Gender)
            .WithMany()
            .HasForeignKey(p => p.GenderId);

        builder.HasOne(p => p.ReleaseForm)
            .WithMany()
            .HasForeignKey(p => p.ReleaseFormId);

        builder.Property(p => p.DateDelivery)
            .HasColumnType("datetime2(7)");
    }
}
