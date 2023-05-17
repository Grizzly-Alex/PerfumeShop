namespace PerfumeShop.Web.Configurations;

public static class ConfigureUtilities
{
    public static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}