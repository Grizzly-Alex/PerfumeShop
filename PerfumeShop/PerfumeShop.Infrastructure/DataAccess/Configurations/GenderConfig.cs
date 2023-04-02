namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class GenderConfig : IEntityTypeConfiguration<CatalogGender>
{
    public void Configure(EntityTypeBuilder<CatalogGender> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
           .UseHiLo("gender_hilo")
           .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
