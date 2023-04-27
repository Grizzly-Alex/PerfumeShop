namespace PerfumeShop.Web.Configurations;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
		services.AddHttpContextAccessor();
		services.AddTransient<ExceptionHandlingMiddleware>();
		services.AddControllersWithViews();
        services.AddScoped<IContentManager, ContentManager>();
        services.AddScoped(typeof(IViewModelService<,>), typeof(ViewModelService<,>));
        services.AddScoped<IViewModelService<CatalogProduct, ProductViewModel>, ProductViewModelService>();
        services.AddScoped<ICatalogViewModelService, CatalogViewModelService>();
        services.AddScoped<IBasketViewModelService, BasketViewModelService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddRazorPages();

        return services;
    }
}
