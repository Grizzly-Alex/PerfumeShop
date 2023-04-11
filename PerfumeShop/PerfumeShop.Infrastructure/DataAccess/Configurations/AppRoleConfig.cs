namespace PerfumeShop.Infrastructure.DataAccess.Configurations;

public sealed class AppRoleConfig : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.HasMany(p => p.UserRoles)
            .WithOne(p => p.Role)
            .HasForeignKey(p => p.UserId)
            .IsRequired();
    }
}
