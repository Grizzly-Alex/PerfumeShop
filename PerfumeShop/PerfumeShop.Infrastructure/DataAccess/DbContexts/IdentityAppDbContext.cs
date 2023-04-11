namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public sealed class IdentityAppDbContext : 
    IdentityDbContext<AppUser, IdentityRole<string>, string,
    IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AppUserConfig());
        modelBuilder.ApplyConfiguration(new AppUserRoleConfig());
    }
}
