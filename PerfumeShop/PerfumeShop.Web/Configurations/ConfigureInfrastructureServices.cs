namespace PerfumeShop.Web.Configurations;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IBasketService, BasketService>();

        return services;
    }
}
