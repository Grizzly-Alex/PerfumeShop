namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public class IdentityAppDbContext : 
    IdentityDbContext<AppUser, IdentityRole, string,
        AppUserClaim, AppUserRole, AppUserLogin,
        AppRoleClaim, AppUserToken>
{
    public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AppUserConfig());
        modelBuilder.ApplyConfiguration(new AppRoleConfig());
    }
}