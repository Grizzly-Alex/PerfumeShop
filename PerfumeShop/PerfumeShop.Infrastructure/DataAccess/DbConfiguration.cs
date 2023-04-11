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

        services.AddDbContext<IdentityAppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

        services.AddIdentity<AppUser, IdentityRole>(options =>
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
    }
}
