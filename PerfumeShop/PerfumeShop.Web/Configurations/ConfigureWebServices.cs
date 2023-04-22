namespace PerfumeShop.Web.Configurations;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddScoped<IContentManager, ContentManager>();
        services.AddTransient(typeof(IViewModelService<,>), typeof(ViewModelService<,>));
        services.AddTransient<IViewModelService<CatalogProduct, ProductViewModel>, ProductViewModelService>();
        services.AddTransient<ICatalogViewModelService, CatalogViewModelService>();
        services.AddTransient<IBasketViewModelService, BasketViewModelService>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddRazorPages();

        return services;
    }
}
