namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class CategoryConfig : IEntityTypeConfiguration<CatalogCategory>
{
    public void Configure(EntityTypeBuilder<CatalogCategory> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
           .UseHiLo("category_hilo")
           .ValueGeneratedNever();


        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
