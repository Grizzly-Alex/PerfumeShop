namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public sealed class IdentityAppDbContext : IdentityDbContext
{
    public DbSet<AppUser> AppUsers { get; set; }

    public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AppUserConfig());
    }
}
