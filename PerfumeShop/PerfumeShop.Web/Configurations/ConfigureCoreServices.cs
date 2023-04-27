namespace PerfumeShop.Web.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
		services.AddScoped<IBasketService, BasketService>();
		services.AddScoped<IBasketQueryService, BasketQueryService>();
		services.AddScoped<IProductQueryService, ProductQueryService>();
		services.AddScoped<IBasketItemQueryService, BasketItemQueryService>();
		services.AddScoped<ICheckoutService, CheckoutService>();

		return services;
    }
}
