namespace PerfumeShop.Web.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
		services.AddTransient<IBasketService, BasketService>();
		services.AddTransient<IBasketQueryService, BasketQueryService>();
		services.AddTransient<IProductQueryService, ProductQueryService>();
		services.AddTransient<IBasketItemQueryService, BasketItemQueryService>();

		return services;
    }
}
