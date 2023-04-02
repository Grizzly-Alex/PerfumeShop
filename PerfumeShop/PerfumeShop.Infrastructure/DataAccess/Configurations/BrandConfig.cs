namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class BrandConfig : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.HasKey(p => p.Id);

		builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
