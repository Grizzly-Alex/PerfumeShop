namespace PerfumeShop.Web.Configurations;

public static class ConfigureAuthenticationSettings
{
    public static IServiceCollection AddAuthenticationSettings(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGoogle()
            .AddFacebook()
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });
        
        return services;
    }
}
