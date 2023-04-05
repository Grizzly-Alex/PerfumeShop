namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class AppUserConfig : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(p => p.FirstName)
            .HasColumnType("varchar(max)")
            .IsRequired(false);

        builder.Property(p => p.LastName)
            .HasColumnType("varchar(max)")
            .IsRequired(false);

        builder.Property(p => p.StreetAddress)
            .HasColumnType("varchar(max)")
            .IsRequired(false);

        builder.Property(p => p.City)
            .HasColumnType("varchar(max)")
            .IsRequired(false);

        builder.Property(p => p.State)
            .HasColumnType("varchar(max)")
            .IsRequired(false);

        builder.Property(p => p.PostalCode)
            .HasColumnType("varchar(max)")
            .IsRequired(false);
    }
}
