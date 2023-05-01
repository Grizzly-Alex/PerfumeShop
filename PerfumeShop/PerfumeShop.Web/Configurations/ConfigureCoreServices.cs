namespace PerfumeShop.Web.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
		services.AddScoped<ShoppingDbInitializer>();
		services.AddScoped<IdentityDbInitializer>();
		services.AddScoped<CatalogDbInitializer>();
		services.AddScoped<IBasketService, BasketService>();
		services.AddScoped<IBasketQueryService, BasketQueryService>();
		services.AddScoped<IProductQueryService, ProductQueryService>();
		services.AddScoped<IBasketItemQueryService, BasketItemQueryService>();
		services.AddScoped<ICheckoutService, CheckoutService>();
		services.AddScoped<ICatalogProductService, CatalogProductService>();

		return services;
    }
}