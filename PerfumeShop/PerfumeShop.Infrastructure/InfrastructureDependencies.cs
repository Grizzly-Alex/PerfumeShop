namespace PerfumeShop.Infrastructure;

public static class InfrastructureDependencies
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
