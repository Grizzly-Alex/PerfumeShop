namespace PerfumeShop.Web.Configurations;

public static class ConfigureAuthenticationSettings
{
    public static IServiceCollection AddAuthenticationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"];
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
            })
            .AddFacebook(options =>
            {
                options.AppId = configuration["Authentication:Facebook:AppId"];
                options.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            })
            .AddTwitter(options =>
            {
                options.ConsumerKey = configuration["Authentication:Twitter:ConsumerKey"];
                options.ConsumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];
            })
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });
        
        return services;
    }
}
