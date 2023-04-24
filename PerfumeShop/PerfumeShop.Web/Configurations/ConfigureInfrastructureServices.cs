namespace PerfumeShop.Web.Configurations;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IBasketService, BasketService>();
        services.AddTransient<IBasketQueryService, BasketQueryService>();
		services.AddTransient<IProductQueryService, ProductQueryService>();
        services.AddTransient<IBasketItemQueryService, BasketItemQueryService>();

		return services;
    }
}
