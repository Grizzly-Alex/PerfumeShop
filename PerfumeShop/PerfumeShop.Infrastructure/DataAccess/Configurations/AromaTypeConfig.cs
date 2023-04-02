namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class AromaTypeConfig : IEntityTypeConfiguration<CatalogAromaType>
{
    public void Configure(EntityTypeBuilder<CatalogAromaType> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
