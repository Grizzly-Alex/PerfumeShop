using ILogger = Serilog.ILogger;
namespace PerfumeShop.Web.Configurations;

public static class WebDependencies
{
    public static ILogger SetLogger(IConfiguration configuration, ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog(logger);

        return logger;
    }

    public static async Task ApplyInitializeDbAsync(IServiceProvider serviceProvider, ILogger logger)
    {
        logger.Information("Initializing Database...");

        using (var scope = serviceProvider.CreateScope())
        {
            var scopedProvider = scope.ServiceProvider;
            try
            {
				await scope.ServiceProvider.GetRequiredService<CatalogDbInitializer>().Initialize();
				await scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>().Initialize();
				await scope.ServiceProvider.GetRequiredService<SaleDbInitializer>().Initialize();
			}
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred initializing the DB.");
            }
        }
    }

    public static void SetServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDataBaseInfrastructure(configuration);
        services.AddStripeInfrastructure(configuration);
        services.AddCookieSettings();
        services.AddAuthenticationSettings();  
        services.AddUtilities();
        services.AddCoreServices();
        services.AddWebServices();       
    }

    public static void SetMiddleware(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseRequestLocalization("en-US", "en-US");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();
        app.MapRazorPages();

        app.MapAreaControllerRoute(
            name: "ShopDefault",
            areaName: "Shop",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapAreaControllerRoute(
            name: "AdminDefault",
            areaName: "Admin",
            pattern: "Admin/{controller}/{action}/{id?}");

		app.UseMiddleware<ExceptionHandlingMiddleware>();
	}
}
