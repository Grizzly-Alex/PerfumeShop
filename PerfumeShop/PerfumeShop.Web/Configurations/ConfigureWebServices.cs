namespace PerfumeShop.Web.Configurations;

public static class ConfigureWebServices
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddTransient(typeof(IViewModelService<,>), typeof(ViewModelService<,>));
        services.AddTransient<IViewModelService<CatalogProduct, CatalogProductViewModel>, ProductViewModelService>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
