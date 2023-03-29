namespace PerfumeShop.Web.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));


        return services;
    }
}
