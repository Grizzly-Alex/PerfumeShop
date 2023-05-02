namespace PerfumeShop.Web.Configurations;

public static class ConfigureUtilities
{
    public static IServiceCollection AddUtilities(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
        services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

        return services;
    }
}