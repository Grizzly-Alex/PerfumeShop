namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class TypeConfig : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
           .UseHiLo("type_hilo")
           .IsRequired();

        builder.Property(p => p.Type)
            .IsRequired()
            .HasMaxLength(100);
    }
}
