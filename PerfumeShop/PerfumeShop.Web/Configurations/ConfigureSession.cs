namespace PerfumeShop.Web.Configurations;

public static class ConfigureSession
{
    public static IServiceCollection AddSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(20);
            options.Cookie.Name = "PerfumeShop.Session";
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        return services;
    }
}