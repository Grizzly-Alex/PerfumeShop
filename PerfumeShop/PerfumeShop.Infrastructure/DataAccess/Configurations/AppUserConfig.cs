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

        #region ForeignKey
        builder.HasMany(e => e.Claims)
            .WithOne(e => e.User)
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();

        builder.HasMany(e => e.Logins)
            .WithOne(e => e.User)
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();

        builder.HasMany(e => e.Tokens)
            .WithOne(e => e.User)
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();

        builder.HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
        #endregion
    }
}
