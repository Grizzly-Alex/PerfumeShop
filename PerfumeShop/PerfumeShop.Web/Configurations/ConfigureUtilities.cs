namespace PerfumeShop.Web.Configurations;

public static class ConfigureUtilities
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
    }
}