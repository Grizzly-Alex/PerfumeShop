namespace PerfumeShop.Infrastructure.DataAccess.DbContexts;

public sealed class CatalogDbContext : DbContext
{
    public DbSet<CatalogBrand> Brands { get; set; }
    public DbSet<CatalogAromaType> AromaTypes { get; set; }
    public DbSet<CatalogGender> Genders { get; set; }
    public DbSet<CatalogReleaseForm> ReleaseForms { get; set; }
    public DbSet<CatalogProduct> Products { get; set; }

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Seeds
        modelBuilder.SeedEnumValues<Gender, CatalogGender>(value => value);
        #endregion

        #region Configurations
        modelBuilder.ApplyConfiguration(new BrandConfig());
        modelBuilder.ApplyConfiguration(new AromaTypeConfig());
        modelBuilder.ApplyConfiguration(new GenderConfig());
        modelBuilder.ApplyConfiguration(new ReleaseFormConfig());
        modelBuilder.ApplyConfiguration(new ProductConfig());
        #endregion
    }
}