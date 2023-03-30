namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class BrandConfig : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
           .UseHiLo("brand_hilo")
           .IsRequired();

        builder.Property(p => p.Brand)
            .IsRequired()
            .HasMaxLength(100);
    }
}
