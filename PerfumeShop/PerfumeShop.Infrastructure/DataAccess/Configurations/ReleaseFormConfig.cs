namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class ReleaseFormConfig : IEntityTypeConfiguration<CatalogReleaseForm>
{
    public void Configure(EntityTypeBuilder<CatalogReleaseForm> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
           .UseHiLo("release_form_hilo")
           .IsRequired();

        builder.Property(p => p.ReleaseForm)
            .IsRequired()
            .HasMaxLength(100);
    }
}
