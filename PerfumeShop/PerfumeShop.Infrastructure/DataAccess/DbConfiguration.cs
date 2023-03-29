namespace PerfumeShop.Infrastructure.DataAccess;

public static class DbConfiguration
{
    public static void SetDbContext(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"));
            options.EnableSensitiveDataLogging();
        });
    }
}
