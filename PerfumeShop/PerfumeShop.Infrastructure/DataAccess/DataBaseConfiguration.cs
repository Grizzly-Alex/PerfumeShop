namespace PerfumeShop.Infrastructure.DataAccess;

public static class DataBaseConfiguration
{
    public static IServiceCollection AddDataBaseSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("CatalogConnection"));
            options.EnableSensitiveDataLogging();
        });

        services.AddDbContext<SaleDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SaleConnection"));
            options.EnableSensitiveDataLogging();
        });

        services.AddDbContext<IdentityAppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            options.EnableSensitiveDataLogging();
        });
            

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireNonAlphanumeric = true;   
            options.Password.RequireLowercase = true; 
            options.Password.RequireUppercase = true; 
            options.Password.RequireDigit = true; 
            options.Password.RequiredLength = 7;   
        })
            .AddDefaultTokenProviders()
            .AddDefaultUI()
            .AddEntityFrameworkStores<IdentityAppDbContext>();

        return services;
    }
}