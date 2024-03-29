﻿namespace PerfumeShop.Web.Configurations;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IRepository< , >), typeof(Repository< , >));
        services.AddScoped<SaleDbInitializer>();
		services.AddScoped<IdentityDbInitializer>();
		services.AddScoped<CatalogDbInitializer>();
		services.AddTransient<IEmailService, EmailService>();
		services.AddScoped<IBasketService, BasketService>();
		services.AddScoped<IBasketQueryService, BasketQueryService>();
		services.AddScoped<IOrderQueryService, OrderQueryService>();
		services.AddScoped<IPhysicalShopQueryService, PhysicalShopQueryService>();
		services.AddScoped<IProductQueryService, ProductQueryService>();
		services.AddScoped<IBasketItemQueryService, BasketItemQueryService>();
		services.AddScoped<ICheckoutService, CheckoutService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICatalogProductService, CatalogProductService>();
		services.AddScoped<IPaymentService, PaymentService>();

		return services;
    }
}