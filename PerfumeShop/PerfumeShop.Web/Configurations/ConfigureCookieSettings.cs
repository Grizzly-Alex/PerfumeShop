namespace PerfumeShop.Web.Configurations;

public static class ConfigureCookieSettings
{
    public const int ValidityMinutesPeriod = 60;
    public const string IdentifierCookieName = "PerfumeShopIdentifier";

    public static IServiceCollection AddCookieSettings(this IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
        });
        services.ConfigureApplicationCookie(options =>
        {
            options.EventsType = typeof(RevokeAuthenticationEvents);
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(ValidityMinutesPeriod);
            options.LoginPath = $"/Identity/Account/Login";
            options.LogoutPath = $"/Identity/Account/Logout";
            options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            options.Cookie = new CookieBuilder
            {
                Name = IdentifierCookieName,
                IsEssential = true 
            };
        });

        services.AddScoped<RevokeAuthenticationEvents>();

        return services;
    }
}
