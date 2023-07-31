namespace PerfumeShop.Web.Configurations;

public static class ConfigureSession
{
    public const int ValidityMinutesPeriod = 100;
    public const string CookieNameForSession = "PerfumeShop.Session";
    public static IServiceCollection AddSession(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(ValidityMinutesPeriod);
            options.Cookie.Name = CookieNameForSession;
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        return services;
    }
}