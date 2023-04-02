namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class ReleaseFormConfig : IEntityTypeConfiguration<CatalogReleaseForm>
{
    public void Configure(EntityTypeBuilder<CatalogReleaseForm> builder)
    {
        builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
	        .IsRequired();

		builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
