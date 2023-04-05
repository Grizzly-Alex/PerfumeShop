namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class AppUserConfig : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(p => p.Id);

		builder.Property(p => p.Id)
	        .IsRequired();

        builder.Property(p => p.FirstName)
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasColumnType("varchar(max)")
            .IsRequired();

        builder.Property(p => p.StreetAddress)
            .HasColumnType("varchar(max)");

        builder.Property(p => p.City)
            .HasColumnType("varchar(max)");

        builder.Property(p => p.State)
            .HasColumnType("varchar(max)");

        builder.Property(p => p.PostalCode)
            .HasColumnType("varchar(max)");
    }
}
