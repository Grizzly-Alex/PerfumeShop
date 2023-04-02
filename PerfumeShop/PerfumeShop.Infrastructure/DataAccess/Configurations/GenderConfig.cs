namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class GenderConfig : IEntityTypeConfiguration<CatalogGender>
{
    public void Configure(EntityTypeBuilder<CatalogGender> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
           .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
