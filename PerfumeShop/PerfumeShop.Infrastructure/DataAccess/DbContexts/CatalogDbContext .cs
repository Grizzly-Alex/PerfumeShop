namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public sealed class CatalogDbContext : DbContext
{

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO
    }
}
